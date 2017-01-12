using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LibHelper : MonoBehaviour
{

    private Text mText;

    private StringBuilder strBuilder;

    // Use this for initialization
    void Start()
    {
        mText = GetComponent<Text>();

        strBuilder = new StringBuilder();

        // Access public static variable
        AccessStaticField();

        // Access public variable
        AccessPrimitiveField();

        // Access public array
        AccessArrayField();

        // Access public Java object
        AccessObjectField();
        
        // Invoke method with no return and no parameter
        InvokeJustMethod();

        // Invoke method with return but pass no parameter
        InvokeMethodJustReturn();

        // Invoke method that returns and pass a primitive parameter
        InvokeMethodPrimitiveParam();

        // Invoke method and more primitive parameter
        InvokeMethodMultiPrimitiveParam();

        // Invoke method and array parameter
        InvokeMethodArrayParam();

        // Invoke method and pass a Java object parameter
        InvokeMethodObjectParam();

        // Invoke method and return an array parameter
        InvokeMethodReturnArray();

        // Try inner enum operation
        TryEnumOperation();

        // Try use interface
        TryInterface();

        // Use Java native array list
        TryJavaArrayList();

        mText.text = strBuilder.ToString();
    }

    // Update is called once per frame
    void Update() {}

    void AccessStaticField() {
        // Get value from the static field
        int sdkVer = new AndroidJavaClass("com.wm4n.android_lib.Util").GetStatic<int>("sdkVer");
        strBuilder.Append(string.Format("sdkVer: {0}\n", sdkVer));

        // Set new value to the static field
		new AndroidJavaClass("com.wm4n.android_lib.Util").SetStatic<int>("sdkVer", 55);
		sdkVer = new AndroidJavaClass("com.wm4n.android_lib.Util").GetStatic<int>("sdkVer");
        strBuilder.Append(string.Format("sdkVer(updated): {0}\n", sdkVer));
    }

    void AccessPrimitiveField() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        // Get primitive value from the non-static field
        long uptime = obj.Get<long>("uptime");
        strBuilder.Append(string.Format("uptime: {0}\n", uptime));

        // Set new value to the non-static field
        obj.Set<long>("uptime", 99887766); // Note this does not change the real uptime
        uptime = obj.Get<long>("uptime");
        strBuilder.Append(string.Format("uptime(updated): {0}\n", uptime));
    }

    void AccessArrayField() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        int[] todayDate = obj.Get<int[]>("todayDate");
        strBuilder.Append(string.Format("todayDate: {0} {1} {2}\n", todayDate[0], todayDate[1], todayDate[2]));

        // 1. Use int[] array and call Set<int[]> >>>> failed and runtime crash
        //
        // int[] newArray = new int[] {2018, 12, 12};
        // obj2.Set<int[]>("thisDate", newArray);

        // 2. Use jvalue to set >>>> failed because cannot find method to set array by field ID
        //
        // jvalue val = new jvalue();
        // IntPtr fieldId = AndroidJNIHelper.GetFieldID(obj2.GetRawClass(), "thisDate", "[I");
		// val.l = AndroidJNIHelper.ConvertToJNIArray (new int[] {2018, 12, 12});

        // 3. Create a new array and set each element than Set<IntPtr> >>>> failed and got this
        // JNI DETECTED ERROR IN APPLICATION: incompatible array type int[] expected long[]: 0x49'
        //
        // IntPtr newArray = AndroidJNI.NewIntArray(3);
        // AndroidJNI.SetIntArrayElement(newArray, 0, 2018);
        // AndroidJNI.SetLongArrayElement(newArray, 1, 12);
        // AndroidJNI.SetLongArrayElement(newArray, 2, 12);
        // obj2.Set<IntPtr>("thisDate", newArray);
        
        // 4. Just convert the new array to IntPtr and set >>>> no crash no exception but value is not set
        //
        // int[] newDate = new int[] {2018, 12, 12};
        // obj2.Set<IntPtr>("thisDate", AndroidJNI.ToIntArray (newDate));

        // 5. Try use object[] following a forum post >>>> failed signature not found
        //
        // int[] newDate = new int[] {2018, 12, 12};
        // object[] a = new object[1];
        // a[0] = newDate;
        // obj2.Set<object[]>("thisDate", a);

        // 6. Try convert to java array before set to object >>>> failed signature not found
        //
        // int[] newDate = new int[] {2018, 12, 12};
        // object[] a = new object[1];
        // a[0] = AndroidJNI.ToIntArray(newDate);
        // obj2.Set<object[]>("thisDate", a);

        // 7. Use object to pass array >>>> failed signature not found
        //
        // object newDate = new int[] {2018, 12, 12};
        // obj2.Set<object>("thisDate", newDate);

        // 8. Use jvalue instead of object >>>> failed signature not found
        //
        // int[] newDate = new int[] {2018, 12, 12};
        // jvalue v = new jvalue();
        // v.l = AndroidJNI.ToIntArray(newDate);
        // obj2.Set<jvalue>("thisDate", v);

        // 9. Use jvalue array >>>> failed signature not found
        //
        // int[] newDate = new int[] {2018, 12, 12};
        // jvalue[] v = new jvalue[1];
        // v[0].l = AndroidJNI.ToIntArray(newDate);
        // obj2.Set<jvalue[]>("thisDate", v);

        // 10. use IntPtr array >>>> failed no such field exception
        //
        // int[] newDate = new int[] {2018, 12, 12};
        // IntPtr[] p = new IntPtr[1];
        // p[0] = AndroidJNI.ToIntArray (newDate);
        // obj2.Set<IntPtr[]>("thisDate", p);
    }

    void AccessObjectField() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        // Get object from the non-static field
        string model = obj.Get<string>("model");
        strBuilder.Append(string.Format("model: {0}\n", model));

        // Or use...
        AndroidJavaObject strObj = obj.Get<AndroidJavaObject>("model");
        strBuilder.Append(string.Format("model: {0}\n", strObj.Call<string>("toString")));

        // Set new value to the non-static field
        obj.Set<string>("model", "ABC-123");
        model = obj.Get<string>("model");
        strBuilder.Append(string.Format("model(updated): {0}\n", model));

        // Or use...
        AndroidJavaObject strObj2 = new AndroidJavaObject("java.lang.String", "XZY-890");
        obj.Set<AndroidJavaObject>("model", strObj2);
        strObj = obj.Get<AndroidJavaObject>("model");
        strBuilder.Append(string.Format("model(updated): {0}\n", strObj.Call<string>("toString")));
    }

    void InvokeJustMethod() {
        new AndroidJavaObject("com.wm4n.android_lib.Util").Call("printLogcat");
    }

    void InvokeMethodJustReturn() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        // Use Call method with <string>
        string str = obj.Call<string>("getModel");
        strBuilder.Append(string.Format("InvokeMethodJustReturn: {0}\n", str));

        // Or use...
        AndroidJavaObject strObj = obj.Call<AndroidJavaObject>("getModel");
        strBuilder.Append(string.Format("InvokeMethodJustReturn: {0}\n", strObj.Call<string>("toString")));
    }

    void InvokeMethodPrimitiveParam() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        object[] args = new object[1];
        args[0] = 3;
        int incResult = obj.Call<int>("incrementBy1", args);
        strBuilder.Append(string.Format("InvokeMethodPrimitiveParam: {0}\n", incResult));
    }

    void InvokeMethodMultiPrimitiveParam() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        object[] args = new object[2];
        args[0] = 3;
        args[1] = 1;
        int addResult = obj.Call<int>("add", args);
        strBuilder.Append(string.Format("InvokeMethodMultiPrimitiveParam: {0}\n", addResult));
    }

    void InvokeMethodArrayParam() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        int[] numbers = new int[] { 1, 3, 5, 7 };
        object[] args = new object[1];
        args[0] = numbers;
        int sum = obj.Call<int>("sum", args);
        strBuilder.Append(string.Format("InvokeMethodArrayParam: {0}\n", sum));
    }

    void InvokeMethodObjectParam() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        object[] args = new object[1];
        args[0] = "hello";
        int length = obj.Call<int>("getLength", args);
        strBuilder.Append(string.Format("InvokeMethodObjectParam: {0}\n", length));

        // or
        args[0] = new AndroidJavaObject("java.lang.String", "hello there");
        length = obj.Call<int>("getLength", args);
        strBuilder.Append(string.Format("InvokeMethodObjectParam: {0}\n", length));
    }

    void InvokeMethodReturnArray() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        int[] todayDate = obj.Call<int[]>("getTodayDate");
        strBuilder.Append(string.Format("InvokeMethodReturnArray: {0} {1} {2}\n", todayDate[0], todayDate[1], todayDate[2]));
    }

    void TryEnumOperation() {
        AndroidJavaClass numberEnum = new AndroidJavaClass ("com.wm4n.android_lib.Util$Number");
		AndroidJavaObject two = numberEnum.GetStatic<AndroidJavaObject> ("Two");
        strBuilder.Append(string.Format("TryEnumOperation: {0}\n", two.Call<string>("toString")));

        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        object[] args = new object[1];
        args[0] = two;
        AndroidJavaObject three = obj.Call<AndroidJavaObject>("rotateEnum", args);
        strBuilder.Append(string.Format("TryEnumOperation: {0}\n", three.Call<string>("toString")));
    }

    class Callback : AndroidJavaProxy
	{
        StringBuilder builder;

		public Callback(StringBuilder builder) : base("com.wm4n.android_lib.Util$ICallback") {
            this.builder = builder;
		}

		void invoke(string str) {
            builder.Append(string.Format("Callback: {0}\n", str));
            Debug.Log(str);
		}
	}

    void TryInterface() {
        AndroidJavaObject obj = new AndroidJavaObject("com.wm4n.android_lib.Util");
        Callback callback = new Callback(strBuilder);

        object[] args = new object[1];
        args[0] = callback;
        obj.Call("setCallback", args);
    }

    void TryJavaArrayList() {
        AndroidJavaObject obj = new AndroidJavaObject("java.util.ArrayList");
        object[] args = new object[1];
        args[0] = new double[] {1.0f, 2.0f, 3.0f};
        obj.Call<bool>("add", args); // <bool> is required even we're not using return type

        int length = obj.Call<int>("size");

        args[0] = 0;
        double[] ret = obj.Call<double[]>("get", args);
        strBuilder.Append(string.Format("TryJavaArrayList: {0} {1:0.0} {2:0.0} {3:0.0}\n", length, ret[0], ret[1], ret[2]));
    }
}
