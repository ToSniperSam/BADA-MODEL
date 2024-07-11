# 毕设 BADA

分为程序，制作UI界面，整合三部分



## 配置环境

python + PyCharm + PyQT5

[PyQt5安装以及使用教程合集(2024) - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/162866700)

qtdesigner

PyUIC



## 程序

python 3.12 先进行初步计算

变量定义

```python
# M 质量
# lo 质量常量 low
# no 质量常量 normal
# hi 质量常量 high
# H 高度 （单位为kt）
# H_normal 高度 （单位为m）
# Vitas 真空速
# Vitas_kt 真空速转化为节
# 局部声速 local_a
# 速度比修正因子 correction_factor
# M 马赫数
# Fuel 燃油流量
# Vcr 速度
# Vc 国际单位速度
# T 温度
# T_ 温度常数
# T_res 温度比
# P 压强
# P_ 压强常数
# P_res 压强比
# Rho 密度
# Rho_ 密度常数
# Rho_res 密度比
# a_  公式参数常量
# K 常量 k
# Vcr_1 速度常量 Vcr1
# Vcr_2 速度常量 Vcr2
# knots_to_maps 速度转换系数 从kt转化为m/s
# Sw 常量 Sw
# CL 计算结果 CL
# CD 计算结果 CD
# CD_0 常量 CD0
# CD_2 常量 CD2
# D 计算结果 D
# fu 计算燃油中间量 η
# Cf1 常量 Cf1
# Cf2 常量 Cf2
# Cfc 常量 Cfc
# Thr 计算结果 Thr
# Mah 马赫数
# M_list 重量数组
# H_list 高度数组
# m_to_nam m/kg 转换为 nam/lb 需要乘 44.4822

# 国际单位制的V = Vcr * knots_to_maps
knots_to_mps = 1.852 * (1000 / 3600)

# 英尺转化为m
m_to_nam = 44.4822

# 导入常量
lo = 107880
no = 150000
hi = 181400
Vcr_1 = 310
Vcr_2 = 310
Rho_ = 1.225
height_convert = 0.3048
a_ = 340.292
K = 1.4
Sw = 283.3
Cf1 = 0.763
Cf2 = 1430
Cfc = 1.0347
CD_0 = 0.014
CD_2 = 0.049
```



## python 代码

1. 导入常量

2. 计算 不同高度下的速度值 v  密度 压强 温度

3. 计算 其他三个结果



## Calculation 2.0

遇到的问题：

```python
1.单位转换问题，1.0版本Vt求出错
2.计算D时，Vitas应该用m/s
3.计算燃料油量系数时，单位应该用kt
4.计算马赫，应注意其是否为0.8
```

核心计算函数Calculation_atmospheric_result ：

<img src="D:\pythonProject\yxb\fig\Minor_cal.png" style="zoom:50%;" />



## Calculation 3.0 更新

因为马赫到0.8之后会出现数据量改变的问题，因此先计算马赫数，从而判断用哪个速度进行计算：

在计算Vitas 之后，立刻用公式计算马赫，判断是否超过0.8，如果超过则改变当前速度

并且引入test函数，可以直接输出全部的测试用例结果

<img src="D:\pythonProject\yxb\fig\test.png" style="zoom: 50%;" />



## Calculation 4.0 更新

新增了两个功能，计算最值问题，并且实现了绘图功能

<img src="D:\pythonProject\yxb\fig\consult.png" style="zoom: 50%;" />

```python
程序1，
选定最佳高度，给定一个质量（分别定为lo, no, hi)，给定一个速度Vitas，通过改变速度，循环找出最大燃油里程，以及对应的最佳高度

程序2，
选定最佳巡航速度，给定一个质量（分别定为lo, no, hi)，给定一个高度H，通过改变马赫数Mah，循环找出最大燃油里程，以及对应的最佳速度
```

```python
    # 找出最大的燃油里程
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
    plt.figure(figsize=(10, 6))
    # 选择与质量M对应的Mah值
    for i in range(len(W_namelist)):
        plt.plot(res_list_x[i], res_list_y[i], label=W_namelist[i])

        # 图例的X坐标和Y坐标，可补充单位
        plt.xlabel('nam/lb')
        plt.ylabel('H (ft)')
        plt.title('Curve Plot of Distance vs Height for Different Weight')
    plt.grid(True)

    # 添加图例
    plt.legend()
    plt.show()
```

实验结果展示：

<img src="D:\pythonProject\yxb\fig\res.png" style="zoom:50%;" />



## 软件部分

PyQT

```python
 # 建一个max表，用于输出答案
        max_x = []
        max_y = []

        # 在每个重量循环中建立两个新的空列表，存储每个重量对应的图像中的横纵坐标
        current_x = []
        current_y = []

        # 对于每个高度，计算其对应的燃油里程
        for H in H_range:

            ...

            # 计算完毕，在横坐标列表中添加燃油里程，纵坐标列表中添加高度H
            current_x.append(My_distance)
            current_y.append(H)

        max_distance = max(current_x)
        # 获取最大燃油里程对应的索引，以便找出最佳的飞行高度
        index_current = current_x.index(max_distance)

        res_list_x.append(current_x)
        res_list_y.append(current_y)
        max_x.append(max_distance)
        max_y.append(current_y[index_current])

        self.canvas_2.figure.clf()  # 清空图表
        self.canvas_2.draw()  # 更新图表显示
        ax = self.figure_2.add_axes([0.15, 0.15, 0.8, 0.8])
        ax.clear()
        ax.plot(res_list_x[0], res_list_y[0])
        ax.set_xlabel("nam/lb")
        ax.set_ylabel('H (ft)')
        ax.set_title('Curve Plot of Distance vs Height for Different Weight')
        self.canvas_2.draw()
```
