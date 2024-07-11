# BADA
This content is part of 614 enterprise.
<br/>
Mainly use python, pyqt5, matplotlib, unity, pandas<br/>
<div align=center><img width="375" height="325" src="https://github.com/ToSniperSam/BADA-MODEL/blob/main/python/pic.png"/></div>

## Python Description
Use Python to implement the calculation of BADA parameters, then use Matplotlib to plot the results, and finally use PyQt5 to create the interface with a UI system.<br/>
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
```

## Crucial Code
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

## Showcase
<div align=center><img width="720" height="440" src="https://github.com/ToSniperSam/BADA-MODEL/blob/main/python/006Awb8Ely1hpobyg7ocpj30wg0jbq7l.jpg"/></div>

## Others
It's a pivotal chapter of my undergraduate life. Some guy will head north to pursue his career, with ideals and freedom in his grasp.
"I'll never forget the support academiclly he provided me over these four years, perhaps..."
Oooops, it remains as a memory.
