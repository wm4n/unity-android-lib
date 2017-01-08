package com.wm4n.android_sample_lib;

/**
 * Sample class shows how to use Android Library API from Unity
 */
public class Calc {

    /**
     * From Unity C#,
     * double pi = new AndroidJavaObject("com.wm4n.android_sample_lib.Calc").CallStatic<double>("PI");
     */
    public final static double PI = Math.PI;

    /**
     * From Unity C#,
     * int prime = new AndroidJavaObject("com.wm4n.android_sample_lib.Calc").Call<int>("SMALLEST_PRIME");
     */
    public final int SMALLEST_PRIME = 2;

    /**
     * From Unity C#,
     * AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_sample_lib.Calc");
     */
    public Calc() {
    }

}
