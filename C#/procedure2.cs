using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class Procedure2 : MonoBehaviour
{
    public InputField input_D;
    public Text lo_procedure_2;
    public Text no_procedure_2;
    public Text hi_procedure_2;

    public Text lo_nam_2;
    public Text no_nam_2;
    public Text hi_nam_2;
    public Text lo_vt_2;
    public Text no_vt_2;
    public Text hi_vt_2;

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

    void Procedure_2(double H)
    {
        double[] Weight = { Lo, No, Hi };
        lo_procedure_2.text = "W = 107880";
        no_procedure_2.text = "W = 150000";
        hi_procedure_2.text = "W = 181400";

        for (int i = 0; i < 3; i++)
        {
            double Opt_Mah = 0;
            double H_Max = 0;
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

            for (double j = 0.3; j < 1.1; j += 0.0001)
            {
                double Mah = j;
                double Vitas_ = Mah * A * Math.Sqrt(T_res);
                double Vitas_kt = Vitas_ / KnotsToMps;
                
                double Rho = Rho_res * Rho_;
                double CL = 2 * Weight[i] * 10 / (Rho * Vitas_ * Vitas_ * Sw);
                double CD = Cd0 + Cd2 * CL * CL;
                double D = 0.5 * CD * Rho * Vitas_ * Vitas_ * Sw;
                double Thr = D;
                
                double fu = Cf1 * (1 + Vitas_kt / Cf2);
                double Fuel = fu * Cfc * Thr;
                double Distance = Vitas_ / Fuel * m_to_nam / 60 * 1000;
                
                if (H_Max < Distance)
                {
                    H_Max = Distance;
                    Opt_Mah = j;
                }
            }
            H_Max = Math.Round(H_Max, 4);
            Opt_Mah = Math.Round(Opt_Mah, 4);
            
            if (i == 0)
            {
                lo_nam_2.text = "Max_nam: " + H_Max;
                lo_vt_2.text = "Opt_Mah: " + Opt_Mah;
            }

            else if (i == 1)
            {
                no_nam_2.text = "Max_nam: " + H_Max;
                no_vt_2.text = "Opt_Mah: " + Opt_Mah;
            }

            else if (i == 2)
            {
                hi_nam_2.text = "Max_nam: " + H_Max;
                hi_vt_2.text = "Opt_Mah: " + Opt_Mah;
            }
        }
        // Debug.Log("最大的燃油距离");
        // Debug.Log(H_Max);
        // Debug.Log("最合适的高度");
        // Debug.Log(Opt_Mah);
    }

    public void get_cal()
    {
        if (string.IsNullOrEmpty(input_D.text))
        {
            lo_procedure_2.text = "输入的值不能为空！！";
        }

        double NumD = double.Parse(input_D.text);
        Procedure_2(NumD);
    }
}
