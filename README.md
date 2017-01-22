## Invoke Android Library API in Unity
This project shows how to invoke Android library API in Unity

### Project Structure
The project is composed of one Android library project (AndroidSampleLib) that implements the demonstrated API and one Unity project (Unity Sample App) that invokes API from the Android library.

* #### Unity Project Structure

    In order to include Android libraries, place the AAR or JAR files under Assets/Plugins/Android/ in the Unity project folders. If there are native Android libraries, place them under Assets/Plugins/Android/libs. Resources should be placed under Assets/Plugins/Android/res because Unity extracts jar from aar when building, which doesn't contain resources.
    
        Unity Sample App/Assets/Plugins/Android/sample_aar.aar
        Unity Sample App/Assets/Plugins/Android/libs/ARMv7/sample_so.so
        Unity Sample App/Assets/Plugins/Android/libs/x86/sample_so.so
        Unity Sample App/Assets/Plugins/Android/res/raw/sample_db.db
        Unity Sample App/Assets/Plugins/Android/res/drawable/sample_bg.xml

* #### Android Project Structure

    For Android, create a library project in Android Studio. e.g. Set the library module's build.gradle to
    
        apply plugin: 'com.android.library'
        
    and remove the applicationId
    
        applicationId "com.pkg.name"
        
    androidTest and test folder can be removed if not needed
    
        AndroidSampleLib/lib/src/androidTest/
        AndroidSampleLib/lib/src/test/
        
    resources folder can be removed if not needed (jar file can not contain resources but we could ask the library to load resources from the Unity project folder e.g. Unity Sample App/Assets/Plugins/Android/res/raw/sample_db.db).
    
        AndroidSampleLib/lib/res/
