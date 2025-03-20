# Procedural Playground
This project focuses on procedural mesh generation and dynamic animations in Unity. Objects are created at runtime with defined vertices and triangles, animated using Lissajous curves, rotated towards targets, and changed color based on angles. Perlin noise is used for vertex animation, adding organic movement
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

### **Demo video**
https://youtu.be/3X4w3-jm1ds

---

### **Implementation Details**

## Step 1 and 2: Procedural mesh creation and secondary object

### **How It Works**
An ObjectA with sphere and a cone in front of it and an ObjectB with a sphere, are procedurally generated at the start of the application, by defining their vertices and triangles based on parameters such as radius, height, and resolution.
### **Implementation Details**
Both Meshes are generated on the Awake method.
#### **Sphere Generation**
The sphere is created by placing vertices based on the latitude and longitude. They are them converted from spherical to 3D space. Triangles connect the vectors to form the surface, and UV mapping is added so the textures can show the rotation animation more clearly.
#### **Cone Generation**
The cone is created with a vertex at the tip and a circular base. The vertices for the base are calculated using trigonometric calculations. The sides are made by connecting the tip and base vertices.
#### **GameObject Setup**
The **MeshManager.cs** is attached to both ObjectA and ObjectB under ObjectsParent in the scene. A boolean can be set from the inspector to enable or disable the generation of each shape. Components such as `MeshFilter` and `MeshRenderer` are added dynamically. Some parameters can be update before runtime to adjust the mesh characteristics.

![Screenshot 2025-01-08 at 09 11 35](https://github.com/user-attachments/assets/4122cbb7-6c37-4800-bc02-00d3d5dc71c0)

### **Challenges**
The lighting seems falling on the cone in an unnatural way, I tried to think of a cause, maybe is related to how I am calculating the normals.

![Screenshot 2025-01-08 at 09 21 08](https://github.com/user-attachments/assets/2ec81211-336c-4a8e-989f-7d0c5a64ed51)

---

## Step 3: Lissajous animation

### **How It Works**
Both ObjectA and ObjectB are animated using Lissajous curves, which can be triggered using the UI toggle in the scene. Those animations are generated by oscillating movements with different frequencies. Those are updated based on parameters like amplitude, frequency and a phase delay, which have been randomly selected between a set range so that the movement does not  seems repetitve, to see a different variation the user can toggle on and off the animation multiple time.
### **Implementation Details**
The animation parements like amplitude, frequency and phaseDelay are randomly set to control the size and speed of the oscillation and add a variaty to the movements. The position of the objects is updated every frame based on the following formulas:
```csharp
X = amplitudeX * sin(frequencyX * time + phaseDelay)
Y = amplitudeY * sin(frequencyY * time)
```
Where time is the current time multiplied by a time multiplier to control the speed of the animation.
#### **GameObject Setup**
The **LissajousMovement.cs** script is attached to both ObjectA and ObjectB. A UI toggle in the scene can be used to start and reset the animation.
### **Challenges**
Testing with multiple paramenters to find the best range to ensure a smooth animation.
### **Assumptions**
Currently while animating both objects can collide by passing through each other, which I assumed was ok for the scope of this task. However, this behaviour could be improved to prevent it.

---

## Step 4: Object rotation

### **How It Works**
ObjectA rotates and moves towards ObjectB with a set angular speed. The animation can be toggled on and off with the UI toggle in the scene.
### **Implementation Details**
ObjectA moves toward the target, in this case ObjectB. The rotation occurs on the Y axis, keeping the Y and Z axes fixed. Once is close enough to the target, it stops.
#### **GameObject Setup**
The **RotationMovement.cs`** script is attached ObjectA. A UI toggle in the scene can be used to start and reset the animation. Rotation speed and movement speed parameters can be update before runtime.

![Screenshot 2025-01-08 at 10 19 50](https://github.com/user-attachments/assets/221da9c5-08f1-460d-a4e5-499ce6526a60)

---

## Step 5: Color change based on angle

### **How It Works**
The color of ObjectA changes depending on the position of ObjectB. When ObjectB is in front of ObjectA, the color is red, and it turns blue when ObjectB is behind.
### **Implementation Details**
ObjectB moves around ObjectA, and the angle between their positions is used to change the color of ObjectA. I calculate the dot product of their forward vector and the direction to ObjectB. This value helps determine how the color should blend between red and blue using  `Color.Lerp`. 
#### **GameObject Setup**
The **ColorChangeBehaviour.cs** script is attached to ObjectA. A UI toggle can be used to start or stop the animation. When the animation is enabled, ObjectB will move in a circular pattern around ObjectA, which triggers the color transition effect on ObjectA based on the angle between them.
### **Assumptions**
I assumed that ObjectB moves automatically in a constant pattern, but this can be adjusted to be based on user input if needed.

---

## Step 6: Mesh vertex animation

### **How It Works**
The vertices of ObjectA’s mesh are animated using Perlin noise. The movement is determined by noise values that change over time.
### **Implementation Details**
The mesh of ObjectA is animated by changing the position of its vertices using Perlin noise. First, I get the original mesh of each child object by accessing its MeshFilter. Then, I make a copy of the mesh to modify, leaving the original mesh untouched. Each frame, I calculate a Perlin noise value for each vertex based on its position and time. The amount of movement is controlled by the amount variable.
#### **GameObject Setup**
The **PerlinNoiseAnimation.cs** script is attached to ObjectA. A UI toggle is used to enable or disable the animation. When the animation is enabled, Perlin noise is used to animate the mesh's vertices. Some parameters can be update before runtime to adjust the animation.

![Screenshot 2025-01-08 at 11 05 59](https://github.com/user-attachments/assets/834ed4aa-ea0f-4725-a349-fbb24dc0056d)







