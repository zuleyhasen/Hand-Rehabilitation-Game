Unity Hand Tracking Game Project
This project integrates hand tracking capabilities into Unity using Python and a webcam. The system allows users to select and play different games or visualize hand Range of Motion (ROM) data. The hand coordinates are detected using a webcam and sent via UDP sockets to Unity, where the game objects' positions are updated in real-time.

Features:
Multiple Games: Choose from Game1, Game2, and Game3.
HandROM: Calculate and display hand Range of Motion values.
Real-time Hand Tracking: Use a webcam to track hand movements.
UDP Communication: Efficiently send hand coordinates to Unity.

Usage:
Start the Unity Project: Initializes Python code and starts the webcam.
Select from the Menu: Choose a game or HandROM to visualize hand data.
Exit: Gracefully exit the system, stopping the Python code and Unity project.

Technologies Used:
Unity
Python
OpenCV (for hand detection)
UDP Sockets

![Ekran görüntüsü 2024-06-19 144251](https://github.com/user-attachments/assets/4f81673f-561c-404e-ba25-bc4f6a06c9ca)
![Ekran görüntüsü 2024-05-24 230123](https://github.com/user-attachments/assets/f7acd059-b06b-47ff-b384-e68b4c92aa70)
