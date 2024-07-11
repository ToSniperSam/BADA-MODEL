using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;


public class Calculation : MonoBehaviour
{
    public InputField input_A;
    public InputField input_B;
    public Text Vitas_;
    public Text CL_;
    public Text CD_;
    public Text FUEL_;
    public Text D_;
    public Text Mah_;
    
    
    // 定义常量
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
        
    // H 为高度，单位ft
    double Vcr;

    void CalculateAtmosphericParameters(double W, double H)
    {
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
        
        double Vc = Vcr * KnotsToMps;
        double local_a = Math.Sqrt((2 / (K - 1)) * T_res);
        
        double correction_factor = (1 + ((K - 1) * (Vc / A) * (Vc / A)) / 2);
        correction_factor = Math.Pow(correction_factor, (K / (K - 1))) - 1;
        correction_factor /= P_res;
        correction_factor += 1;
        
        double Vitas = A * local_a * Math.Sqrt(Math.Pow(correction_factor, (K - 1) / K) - 1);

        // 将真空速转换为节
        double Vitas_kt = Vitas / KnotsToMps; // 假设C
        double Rho = Rho_res * Rho_;
        double CL = 2 * W * 10 / (Rho * Vitas * Vitas * Sw);
        double CD = Cd0 + Cd2 * CL * CL;
        double D = 0.5 * CD * Rho * Vitas * Vitas * Sw;
        double Thr = D;

        double fu = Cf1 * (1 + Vitas_kt / Cf2);
        double Fuel = fu * Cfc * Thr;

        double MAH = Vitas / A / Math.Sqrt(T_res);
        double Mah = MAH;
        
        if (MAH >= 0.8)
        {
            Vitas = 0.8 * A * Math.Sqrt(T_res);
            Vitas_kt = Vitas / KnotsToMps;
            Mah = 0.8;
        }

        Vitas = Math.Round(Vitas, 4);
        CL = Math.Round(CL, 4);
        CD = Math.Round(CD, 4);
        Fuel = Math.Round(Fuel, 4);
        Mah = Math.Round(Mah, 4);
        D = Math.Round(D, 4);

        Vitas_.text = "Vitas: " + Vitas;
        CL_.text = "CL: " + CL;
        CD_.text = "CD: " + CD;
        FUEL_.text = "Fuel: " + Fuel;
        D_.text = "D: " + D;
        Mah_.text = "Mah: " + Mah;

        // Debug.Log("M:");
        // Debug.Log(W);
        // Debug.Log("H:");
        // Debug.Log(H);
        // Debug.Log("Vitas:");
        // Debug.Log(Vitas);
        // Debug.Log("Vitas/kt:");
        // Debug.Log(Vitas_kt);
        // Debug.Log("CL:");
        // Debug.Log(CL);
        // Debug.Log("CD:");
        // Debug.Log(CD);
        // Debug.Log("fu:");
        // Debug.Log(fu);
        // Debug.Log("D:");
        // Debug.Log(D);
        // Debug.Log("Mah:");
        // Debug.Log(Mah);
    }


    
    public void calculation()
    {
        if (string.IsNullOrEmpty(input_A.text) || string.IsNullOrEmpty(input_B.text))
        {
            Vitas_.text = "输入的值不能为空！！";
        }

        double NumA = double.Parse(input_A.text);
        double NumB = double.Parse(input_B.text);
        CalculateAtmosphericParameters(NumA, NumB);
    }
    
    public void Click_to_cal()
    {
        calculation();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
