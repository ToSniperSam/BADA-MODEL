# coding:utf-8
import math

# 导入matplotlib模块并使用Qt5Agg
import matplotlib
from yxb.calculation.calculation import knots_to_mps

matplotlib.use('Qt5Agg')
# 使用 matplotlib中的FigureCanvas (在使用 Qt5 Backends中 FigureCanvas继承自QtWidgets.QWidget)
from matplotlib.backends.backend_qt5agg import FigureCanvasQTAgg as FigureCanvas
from PyQt5 import QtWidgets
from PyQt5.QtWidgets import *
import matplotlib.pyplot as plt
import sys
import numpy as np
from numpy import *

lo = 107880
no = 150000
hi = 181400
Vcr_1 = 310
Vcr_2 = 310
Rho_ = 1.225
# 国际单位制的V = Vcr * knots_to_maps
knots_to_mps = 1.852 * (1000 / 3600)
m_to_nam = 44.4822
# 英尺转化为m
height_convert = 0.3048
a_ = 340.292
K = 1.4
Sw = 283.3
Cf1 = 0.763
Cf2 = 1430
Cfc = 1.0347
CD_0 = 0.014
CD_2 = 0.049

class App(QtWidgets.QDialog):
    def __init__(self, parent=None):
        # 父类初始化方法
        super(App, self).__init__(parent)

        self.initUI()

    def initUI(self):
        self.setWindowTitle('PyQt5结合Matplotlib绘制函数图像')
        # 几个QWidgets
        self.figure = plt.figure()
        self.canvas = FigureCanvas(self.figure)
        self.button_plot = QtWidgets.QPushButton("绘制函数图像")
        self.button_exit = QtWidgets.QPushButton("清空图像")
        self.comboBox = QComboBox()
        self.comboBox.addItems(["Lo", "No", "Hi"])
        self.comboBox.currentIndexChanged.connect(self.comboBoxValueChanged)
        # 创建输入框
        self.input_box = QLineEdit(self)

        # 连接事件
        self.button_plot.clicked.connect(self.plot_)
        self.button_exit.clicked.connect(self.deletes)


        # 设置布局

        layout = QtWidgets.QVBoxLayout()
        layout.addWidget(self.canvas)
        layout.addWidget(self.input_box)
        layout.addWidget(self.comboBox)
        layout.addWidget(self.button_plot)
        layout.addWidget(self.button_exit)
        self.setLayout(layout)

    # 连接的绘制的方法
    def plot_(self):
        input_value = float(self.input_box.text())
        M = self.comboBox.currentText()
        # 重量组名字列表
        W_namelist = ['low', 'normal', 'high']

        # 组别索引
        W_index = 0
        M_list = [lo, no, hi]
        res_list_x = []
        res_list_y = []
        H_range = np.arange(3000, 43000, 1)
        Vitas = input_value
        Vitas_kt = Vitas / knots_to_mps

        for M in M_list:
            # 在每个质量循环中建立两个新的空列表，存储每个质量对应的图像中的横纵坐标
            current_x = []
            current_y = []
            # 对于每个高度，计算其对应的燃油里程
            for H in H_range:
                T_res = 0
                P_res = 0
                Rho_res = 0
                H_normal = H * height_convert
                # 输入的 H 为 kt 单位
                if 0 <= H <= 2999:
                    Vcr = min(Vcr_1, 170)
                elif 3000 <= H <= 5999:
                    Vcr = min(Vcr_1, 220)
                elif 6000 <= H <= 13999:
                    Vcr = min(Vcr_1, 250)
                else:
                    Vcr = Vcr_2

                if H_normal <= 11000:
                    # 计算温度比T0
                    T_res = (1 - 2.25577E-05 * H_normal)
                    # 计算压力比∂
                    P_res = (1 - 2.25577E-05 * H_normal) ** 5.25588
                    # 计算密度比σ
                    Rho_res = (1 - 2.25577E-05 * H_normal) ** 4.25588

                elif 11000 <= H_normal <= 20000:
                    T_res = 0.75186535
                    P_res = 0.2233609 * math.exp((11000 - H_normal) / 6341.62)
                    Rho_res = 0.2970756 * math.exp((11000 - H_normal) / 6341.62)

                Rho = Rho_res * Rho_
                CL = 2 * M * 10 / (Rho * Vitas * Vitas * Sw)
                CD = CD_0 + CD_2 * CL * CL
                D = 0.5 * CD * Rho * Vitas * Vitas * Sw
                Thr = D

                # 计算燃油流量Fuel
                fu = Cf1 * (1 + Vitas_kt / Cf2)
                Fuel = fu * Cfc * Thr / 1000

                # 海里里程 /60 表示从分钟到秒，*表示转换m/kg到nam/lb
                Distance = Vitas / Fuel * m_to_nam / 60
                My_distance = int(Distance * 1000) / 1000

                # 计算完毕，在横坐标列表中添加燃油里程，纵坐标列表中添加高度H
                current_x.append(My_distance)
                current_y.append(H)

            max_distance = max(current_x)
            # 获取最大燃油里程对应的索引，以便找出最佳的飞行高度
            index_current = current_x.index(max_distance)

            # 打印计算结果
            print('质量为 ' + W_namelist[W_index] + ' 的组别中：最大燃油里程为：{}'.format(
                max_distance) + ' nam/lb' + ' 对应的最佳飞行高度为：{}'.format(current_y[index_current]) + ' ft')
            res_list_x.append(current_x)
            res_list_y.append(current_y)

            # 组别类型索引+1
            W_index += 1

        # 绘图代码
        # 选择与质量M对应的Mah值
        # for i in range(len(W_namelist)):
        #     ax = self.figure.add_axes([0.1, 0.1, 0.8, 0.8])
        #     ax.clear()
        #     ax.plot(res_list_x[i], res_list_y[i])
        #
        #     # 图例的X坐标和Y坐标，可补充单位
        #     plt.xlabel('nam/lb')
        #     plt.ylabel('H (ft)')
        #     plt.title('Curve Plot of Distance vs Height for Different Weight')
        #     self.canvas.draw()
        ax = self.figure.add_axes([0.1, 0.1, 0.8, 0.8])
        ax.clear()
        ax.plot(res_list_x[0], res_list_y[0])
        ax.plot(res_list_x[1], res_list_y[1])
        ax.plot(res_list_x[2], res_list_y[2])

        self.canvas.draw()

    def deletes(self):
        self.canvas.figure.clf()  # 清空图表
        self.canvas.draw()  # 更新图表显示

# 运行程序
if __name__ == '__main__':
    app = QtWidgets.QApplication(sys.argv)
    main_window = App()
    main_window.show()
    app.exec()
