package com.wm4n.android_lib;

import android.os.Build;
import android.os.SystemClock;

import java.util.Calendar;

/**
 * Sample class shows how to use Android Library API from Unity
 */
public class Util {

    /**
     * Access public static variable
     *
     * From Unity C#,
     * int sdkInt = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .GetStatic<int>("sdkVer");
     */
    public static int sdkVer = Build.VERSION.SDK_INT;

    /**
     * Access public variable
     *
     * From Unity C#,
     * long thisYear = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Get<long>("uptime");
     */
    public long uptime = SystemClock.uptimeMillis();

    /**
     * Access public array
     *
     * From Unity C#,
     * int[] todayDate = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Get<int[]>("todayDate");
     */
    private static final Calendar calendar = Calendar.getInstance();
    public int[] todayDate = new int[] {calendar.get(Calendar.YEAR), calendar.get(Calendar.MONTH)+1, calendar.get(Calendar.DAY_OF_MONTH)};

    /**
     * Access public Java object
     *
     * From Unity C#,
     * AndroidJavaObject model = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Get<AndroidJavaObject>("model");
     */
    public String model = Build.MODEL;

    /**
     * Using public constructor
     *
     * From Unity C#,
     * AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
     */
    public Util() {
    }

    /**
     * Invoke method with no return and no parameter
     *
     * From Unity C#,
     * new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Call("printLogcat");
     */
    public void printLogcat() {
        android.util.Log.i("Util", "printLogcat() invoked!");
    }

    /**
     * Invoke method that returns and pass no parameter
     *
     * From Unity C#,
     * string result = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Call<string>("getDeviceModel");
     */
    public String getModel() {
        return model;
    }

    /**
     * Invoke method with primitive parameter
     *
     * From Unity C#,
     * object[] arg = new object[1];
     * arg[0] = 3;
     * int result = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Call<int>("incrementBy1", args);
     */
    public int incrementBy1(int num) {
        return ++num;
    }

    /**
     * Invoke method and more primitive parameter
     *
     * From Unity C#,
     * object[] arg = new object[2];
     * arg[0] = 3;
     * arg[1] = 1;
     * int result = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Call<int>("add", args);
     */
    public int add(int num1, int num2) {
        return num1 + num2;
    }

    /**
     * Invoke method and pass an array parameter
     *
     * From Unity C#,
     * int[] numbers = new int[] {1, 3, 5, 7};
     * object[] arg = new object[1];
     * args[0] = numbers;
     * int sum = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Call<int>("sum", args);
     */
    public int sum(int[] intArray) {
        int sum = 0;
        for(int i = 0; i < intArray.length; i ++) {
            sum += intArray[i];
        }
        return sum;
    }

    /**
     * Invoke method and pass a Java object parameter
     *
     * From Unity C#,
     * object[] arg = new object[1];
     * args = "hello";
     * int length = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Call<int>("getLength", args);
     */
    public int getLength(String param) {
        if(null != param)
            return param.length();
        return 0;
    }

    /**
     * Invoke method and return an array
     *
     * From Unity C#,
     * int[] date = new AndroidJavaObject("com.wm4n.android_lib.Util")
     *      .Call<int[]>("getTodayDate");
     *
     */
    public int[] getTodayDate() {
        return todayDate;
    }

    public enum Number {
        One, Two, Three, Four, Five
    }

    public Number rotateEnum(Number num) {
        if(Number.One == num) return Number.Two;
        else if(Number.Two == num) return Number.Three;
        else if(Number.Three == num) return Number.Four;
        else if(Number.Four == num) return Number.Five;
        else return Number.One;
    }

    public interface ICallback {
        void invoke(String str);
    }

    public void setCallback(ICallback callback) {
        if(null != callback) {
            callback.invoke("Hello");
        }
    }

}
