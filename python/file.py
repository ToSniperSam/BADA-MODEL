import sys
import os
from PyQt5.QtWidgets import QApplication, QWidget, QPushButton

class MyWidget(QWidget):
    def __init__(self):
        super().__init__()

        self.initUI()

    def initUI(self):
        # 创建一个按钮
        self.btn = QPushButton('Open File', self)
        self.btn.clicked.connect(self.openFile)
        self.btn.setGeometry(50, 50, 100, 30)

    def openFile(self):
        # 获取当前目录
        current_dir = os.path.dirname(os.path.realpath(__file__))
        # 拼接文件路径
        file_path = os.path.join(current_dir, 'output.xlsx')
        # 使用默认程序打开文件
        os.startfile(file_path)

if __name__ == '__main__':
    app = QApplication(sys.argv)
    widget = MyWidget()
    widget.show()
    sys.exit(app.exec_())
