using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class Procedure1 : MonoBehaviour
{
    public InputField input_C;
    public Text lo_procedure_1;
    public Text no_procedure_1;
    public Text hi_procedure_1;

    public Text lo_nam_1;
    public Text no_nam_1;
    public Text hi_nam_1;
    public Text lo_h_1;
    public Text no_h_1;
    public Text hi_h_1;
    
    const int Lo = 107880;
    const int No = 150000;
    const int Hi = 181400;
    const double Vcr1 = 310;
    const double Vcr2 = 310;
    const double Rho_ = 1.225;
    const double KnotsToMps = 1.852 * (1000.0 / 3600);
    const double HeightConvert = 0.3048;
    const double A = 340.292;
    const double K = 1.4;
    const double Sw = 283.3;
    const double Cf1 = 0.763;
    const double Cf2 = 1430;
    const double Cfc = 1.0347;
    const double Cd0 = 0.014;
    const double Cd2 = 0.049;
    const double m_to_nam = 44.4822;
    
    double T_res = 0.0;
    double P_res = 0;
    double Rho_res = 0;
    double Vcr;
    
    void Procedure_1(double Vitas)
    {
        double[] Weight = {Lo, No, Hi};
        double Vitas_kt = Vitas / KnotsToMps;
        lo_procedure_1.text = "W = 107880";
        no_procedure_1.text = "W = 150000";
        hi_procedure_1.text = "W = 181400";
        
        for (int i = 0; i < 3; i++)
        {
            double Max_x = 0;
            double Optimal_y = 0;
            double[] res_x = new double[100000];
            double[] res_y = new double[100000];
            for (int j = 3000; j < 43000; j++)
            {
                double H = j;
                double H_normal = H * HeightConvert;
                if (0 <= H && H <= 2999)
                {
                    Vcr = Math.Min(Vcr1, 170);
                }
                else if (3000 <= H && H <= 5999)
                {
                    Vcr = Math.Min(Vcr1, 220);
                }
                else if (6000 <= H && H <= 13999)
                {
                    Vcr = Math.Min(Vcr1, 250);
                }
                else
                {
                    Vcr = Vcr2;
                }
        
                if (H_normal <= 11000)
                {
                    // 计算温度比T0
                    T_res = (1 - 2.25577E-05 * H_normal);
                    // 计算压力比∂
                    P_res = Math.Pow((1 - 2.25577E-05 * H_normal), 5.25588);
                    // 计算密度比σ
                    Rho_res = Math.Pow((1 - 2.25577E-05 * H_normal), 4.25588);
                }
                else if (11000 <= H_normal && H_normal <= 20000)
                {
                    T_res = 0.75186535;
                    P_res = 0.2233609 * Math.Exp((11000 - H_normal) / 6341.62);
                    Rho_res = 0.2970756 * Math.Exp((11000 - H_normal) / 6341.62);
                }
                
                double Rho = Rho_res * Rho_;
                double CL = 2 * Weight[i] * 10 / (Rho * Vitas * Vitas * Sw);
                double CD = Cd0 + Cd2 * CL * CL;
                double D = 0.5 * CD * Rho * Vitas * Vitas * Sw;
                double Thr = D;
                
                double fu = Cf1 * (1 + Vitas_kt / Cf2);
                double Fuel = fu * Cfc * Thr;
                double Distance = Vitas / Fuel * m_to_nam / 60 * 1000;
                if (Max_x < Distance)
                {
                    Max_x = Distance;
                    Optimal_y = j;
                }
            }

            Max_x = Math.Round(Max_x, 4);
            if (i == 0)
            {
                lo_nam_1.text = "Max_nam: " + Max_x;
                lo_h_1.text = "Opt_h: " + Optimal_y;
            }
            else if (i == 1)
            {
                no_nam_1.text = "Max_nam: " + Max_x;
                no_h_1.text = "Opt_h: " + Optimal_y;
            }
            else if (i == 2)
            {
                hi_nam_1.text = "Max_nam: " + Max_x;
                hi_h_1.text = "Opt_h: " + Optimal_y;
            }
            Debug.Log("最大的燃油距离");
            Debug.Log(Max_x);
            Debug.Log("最合适的高度");
            Debug.Log(Optimal_y);
        }

    }

    public void get_cal()
    {
        if (string.IsNullOrEmpty(input_C.text))
        {
            lo_procedure_1.text = "输入的值不能为空！！";
        }
        
        double NumC = double.Parse(input_C.text);
        Procedure_1(NumC);
    }
    
}