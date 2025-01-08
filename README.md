# TestTask_XARlabs
 This project has been developed following the requirements for a Test task for the Unity developer position at XARLabs.

 ## **Setup - Unity Version:  6000.032f1 or above**

### **Running the Project**

#### **Desktop Demo:**
1. Open the project in Unity Editor.
2. Load the scene **[Main]** from `Assets/Scenes`.  
   - Ensure the scene is added to `File > Build Settings > Scenes in Build`.
3. Press **Play** to run the scene.  
   - Use the toggles in the UI to switch between animations.

#### **AR Demo (iOS):**
**Device Requirements:** iOS device running iOS 13 or later.
1. Switch the platform to iOS:
   - Go to `File > Build Settings > iOS > Switch Platform`.
2. Configure settings:
   - Enable **Development Build** under `Platform Settings`.
   - Ensure ARKit is enabled in `Edit > Project Settings > XR Plug-in Management > iOS > Apple ARKit`.
3. Build and run:
   - Click **Build and Run** to generate an Xcode project.
   - Open the project in Xcode, set up a provisioning profile, and build the app on an ARKit-compatible iOS device.
   - Run the app on the device.

---

### **Project Dependencies**

This project uses the following Unity packages:  
- **AR Foundation 6.0.4**  
- **Apple ARKit XR Plugin 6.0.4**  

The packages are already included and can be checked or installed by:
1. Open Unity Editor and go to `Window > Package Manager`.
2. Search for the package name under the `Unity Registry`.
3. Install the required version.

---
### **Implementation Details**

## Step 1: Procedural Mesh Creation

### **How It Works**
At the start of the application in the Awake, the MeshManager.cs procedurally generates a sphere and a cone in front of it, by defining their vertices and triangles based on parameters such as radius, height, and resolution.

### **Implementation Details**

#### **Sphere Generation**
The sphere is created by placing vertices based on the latitude and longitude. They are them converted from spherical to 3D space. Triangles connect the vectors to form the surface, and UV mapping is added so the textures can show the rotation animation more clearly.
#### **Cone Generation**
The cone is created with a vertex at the tip and a circular base. The vertices for the base are calculated using trigonometric calculations. The sides are made by connecting the tip and base vertices.
#### **GameObject Setup**
The MeshManager.cs is attached to both ObjectA and ObjectB under ObjectsParent in the scene. A boolean can be set from the inspector to enable or disable the generation of each shape. Components such as `MeshFilter` and `MeshRenderer` are added dynamically. Some parameters can be update before runtime to adjust the mesh characteristics.

![Screenshot 2025-01-08 at 09 11 35](https://github.com/user-attachments/assets/4122cbb7-6c37-4800-bc02-00d3d5dc71c0)

### **Challenges**

The lighting seems falling on the cone in an unnatural way, I tried to think of a cause, maybe is related to how I am calculating the normals.
![Screenshot 2025-01-08 at 09 21 08](https://github.com/user-attachments/assets/2ec81211-336c-4a8e-989f-7d0c5a64ed51)


---





