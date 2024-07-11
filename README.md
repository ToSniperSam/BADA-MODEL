# BADA
An areoplane demo made for 614.
<br/>
This content is part of his enterprise, involving computer science and mathematics closely related to my course.
<br/>
<div align=center><img width="375" height="325" src="https://github.com/ToSniperSam/BADA-MODEL/blob/main/python/pic.png"/></div>
## Python description
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
