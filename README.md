# Flight Simulator Project
This application simulates a flight for the flight inspector & pilots that need to study or inspect the flight.
The flight data receive via *.txt* file, also this project heavily controlled by events so we based most of our code on **MVVM**  **Architectural Pattern**
- **Pay attention** when we use the linear detector the program **takes time!**
- **Pay attention** before using the app the user must start the flight gear simulation via the menu on the right side of the app.

## Flight Data
- Current & Length time of the flight.
- Steering mode via Joy-Stick, Sliders and Flight Instruments.
- List of all the elements of the flight.
- Information about each element of the flight shown by 3 charts.	
	> Regular chart that shows the **value** by the **time**.
	> **Linear regression** chart for each element .
	> Third chart represent the **most correlative feature** that exists.
- Graphic display of the plain over the earth.
- Extra window to represent the DLL file and his detection on the flight. 
- The DLL file must use the program API.
	> API- getFunc(),getAnomalyCount(),getDiscription(),getTimeStep(),
	> createSimpleAnomalyDetectorInstance(),detect(),learn().
	* createSimpleAnomalyDetectorInstance - makes a new instance of the Anomaly Detect.
	* learn- (offline learning stage) setting the standard flight data.
	* detect- (online anomaly detection) checking if the data anomalies.
	* getAnomalyCount- returning  the number of anomalies.
	* getDiscription- returning the names of the anomaly. 
	* getTimeStep - returning the time step of the anomaly.
	* getFunc() - returning  the graphical view of the detector chart. 

### special features
- Menu options to show the flight or detect the flight.
- joy-Stick display, allocates the aileron & elevator data.
- Sliders display the rudder & throttle in a scale of the interval [0,1].
- Flight Instruments displays the level line of the plain.
- After the first run the location of the files will be saved, so to run the same files will be easier.
- To set a new current flight time we can use a slider or enter the percentage of the flight : 
	> To start from the beginning we enter 0 as start at 0%
	> To jump to the middle we enter 50 as 50% of the flight
- Graph that shows the anomaly detection on the loaded DLL file.

## Math knowledge
- At the Class *JoyStick_view.xaml.cs & DataViewer.xaml.cs*  we implement a function that re-maps a number from one range to another so the Joy-Stick stays in the range of his base, the mathematical explanation can be found [her]( https://stackoverflow.com/questions/5731863/mapping-a-numeric-range-onto-another)
- To calculate the correlative feature we used [probability theory](https://en.wikipedia.org/wiki/Probability_theory "Probability theory") and [statistics](https://en.wikipedia.org/wiki/Statistics "Statistics") function as [Pearson](https://en.wikipedia.org/wiki/Pearson_correlation_coefficient), [Variance](https://en.wikipedia.org/wiki/Variance), [Covariance](https://en.wikipedia.org/wiki/Covariance) and [Linear Regression](https://en.wikipedia.org/wiki/Linear_regression)

## Files 
### Main Classes
- *FlightController* - The **main** class of the project, sending the data to the simulator, and notifying by an event that the data of the flight changed  
- *PassData_VM* - Part of the **MVVM** Architectural Pattern (a model that created for the view) and contains all the raw data as **property** that we want to display in the view classes 
- *AnomalyDetector* - This class handles the DLL files and can detect flight anomalies 
- The *LineChart_model*, *LineChart_VM*, *LineChart_view*, used **MVVM** to calculate data and binding theme to the charts
- The *FileHandler*, *FileLoader_VM*, *FileLoader* use **MVVM** to bind the file location data to be used in the program
- The *MediaPlayer*, *MediaController_VM*, *MediaController* , use **MVVM** to bind the data pace and setting the flight position of the record.
-  The *JoyStick_view*, *PassData_VM*, *FlightController* use **MVVM** architectural  that bind to the data of the flight, so we get a moving Joy-Stick that shows the current steering of the flight.
- *AnomalyUC*, *DLLDataParser*, *Anomalies_VM* use **MVVM** . The DLLDataParser is the model - it takes all the needed information from the dll which is dynamically loaded and eventually the data is presented in the *AnomalyUC* - the view of the DLL information. The model takes as input float numbers that represents the x and y of the anomaly points, string that represents pairs of  names of the features and another string that represents the function that is needed to be drawn(in out dlls for example it is a function of a line or a function of the needed circle).
- DataCalculations - make all the math calculations that needed for the charts
- CSVParser - interface so we can added another way to read the data.
- Also we implement **Singleton Pattern** on the following classes: *FlightController*, *mediaController* as we want to make sure that only one class to be created, that control all the flights (only one flight per time).
- FlightControllerEventArgs - build in class that used in event (data change) to read the data of a current line.

## Installations & Settings

- Download [VS_community](https://visualstudio.microsoft.com/vs/community/), .net framework version 5.0, Desktop development with c++
- Add to NuGet Packages : OxyPlot.wpf (ver. 2.0.0)
- Add to NuGet Packages : ncalc (ver. 1.3.8)
- Download [flightSimulator](https://www.flightgear.org/) 
- open app --> settings --> at Additional Settings past:
	> --generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
	--fmd=null
- when running the simulation hit **fly** button on the lower left corner.
- All the **CSV** files must be without labels 
- before using the app the user must start the flight gear simulation via the menu on the right side.
 
## UML 
[Link](https://plantuml-server.kkeisuke.dev/svg/jHhRafiwyfslu76t79_Qd4TNQlOGct6ymJPbR2TxAue1sQCQ32wGFPcTpRzljyH5Ge9najgNyT1tTRTQhOO_IXOLx79AhVA9PkUeY4vsd4PbwJ1Mq8SB8uiupmBw7sB_nRfFwV6HJVEC5NcQaiBEnsFxVZvDASbO3vO3Pn6B_AWeIM6cMfA4HYtCycX6fezWM7ZA4v9AajuWkaKG6ZEDqlYIHepcMMdTqvHuUPIG8jmlXIw0V8go14LCWxqmtVxmtzz-qnUqMEMC7bxy8Z-JWhs0jkn8aefObZn8sJNyBQcrTJyW4qtGGKQtkLS8g5CSItdDBRv1I_wzKlBCPRKWqDSweF5hwm-PiiFAepMuE4NDROH2gEL3TcFycc1t4sj8ciHiBIEQVQ5PaZ-RvNJnKjWgi0XJGoR2uwVHYtbVeD8Tv5m5aQ8iUApWCNzsQ56U2TYB0cHpDKwPMh2cZB9BGcvahQWvwumM9CRjT1jlGywP_nuzRs21FcNnustyCeVrj_mbO3H-QdDma5yXjxhLm5orD268smC_uDf_DTqTb9OX5Y-rah1NoxR7rzD4aGAWGppHkG5-7uS9KEtEy9SaV_hpnBxcD45CIHZtZWGR1OJjbpBHAJhZOv9V7b9YVQ_CqKhqgpMs3ySmvdL-qbRyiNtcxfbe3hC1TyB4d8WVoUgndS1Yj29iZSVMo4wXE431RMmSsMV8REdnIDZgSdeWnVg06xj45EMcP7pJDgIwjxcdgiqiIGpe2OqWWhn5q0oEnYmc4nj-8Lzq_sEmgnh1ZGVRkiioB9JJ-Jr-Gva4gwkD4gNGGf1QwtdzhKj96xOgB12jEbBR-5o7gcG5YI2nLeGzvyLJm1-jyND16HeYdh_m9rXx2SlCO8UYueSyJwrH1K3_2410CicDqpGlIVEePbAxF3Ijl8O7g8Jere9cHskSOXphnt_-snhZIiD7WiS4Ef3oGX4LBnyWqEygkZj1zl5b5Ps8GYJmxnfvG8aqiWQUQzmYj5ZdhLMCvYAYzqL-mcn3wifgH1b0njoinGXZMmvSQvj2FEAOtbrZlriWX1I7A2Psquoy3Yx0bbVGCREEJUTiWT2KOYU0g02FX6fZ9UGGNL8cWmvmQ72nrNCchxg2qN9poJ9GMENBMDjFtQR3gXompLcKRkb9yIt0fvUYW5JkOjG2SSotUVjaFCZR4W7rItQ-SVqZVVsZplf7XlMFkkkNesXkC5wjtWhSD5ol8kttKNeXvL3axIRhMb_PYX6IJDjkD7SRvjU-qyS69Zn10DK998TtinX0Q7XAIsQs6nG_aSG3_0TUBtAmzEuE-HW4F63HwImyoyysk9CavJQFeR0sKEEnOcDaRM7i6-a0fABOoaVc4VTFoSkUqSAMYtkXmphDYoB2moLHTpwYiUn5nOOSrmSa8sZnbTDiB-UKj3hUATfGB7ye1PyGFsJVAAuSGhoeP87mtH03TvgoDK18fWLKa3Md5T1qF3knEm3btD8p3lknfW5AvVuUSp0lJX63v5Myg4TiZEl1PYeCI2fwp6wWTStx9TVBKrvMBMuCWCduATEgFRyNkSPznW6_gu0wyTxCkT65hw1AiWhuYwxbtBuQl_qor4E8KDoI0ya356sQUJz0kr9T9bHb11A-bcBYF_FKxjFHrCVsAYKgrBTJUexOenmYQ3Hg42q4w73i4aLPVehI5w3rEvYRWgTl4p2a8sbqEFgg7IDTtQWoP6emvAVpm5Ry9UxTpVvIh-BehqSIFztJZ9RodM4yHV3Yi3wJJ5tw0yj2K9rzFQKXD9dnam7v0FrAFzBzwfgHvUNXH5a_exDQBnrluGOQYRvWlEMBG-nnebp1Vpx6aAQ6C0EqZRG4D0GReF_VU1l6FAB0cQzajIlavckHmKaXrodSd7AhLO6YIqawi6FESXdqPWa47hYkyWxhBSUpshwEFpNJwxQWGahDeuQEMvmazLTZ8JLLyiI9no5kV6--rlzIxQ_3EOwgsSx4Fi2zcXzJKJMpaK1uqO8LpyXH0g8fAkICmuGMPEDr8I51IiyAa3qMEMEfhA2u90akj3K2Tc6HPn84Jkfhn7A9wFRjgNS0RTuqQUDehk1GpHi8_MHm2C8tNPyWq5VTKa43pJLr-chkGMZNGmZJNSBBHjTrpOvlXeXraeq2dEoBx9f3fobxwD39ENCM7jfgEL4hPvDYbKfUQfMer1TPvROxRDHDyngxK-J4uBCvrGm3Ky3aQNADqcRErTfV7ofOsCp73QzlPXIMEMmXUywmgg3fMBpFO2SKbYdb1nHUvWo9tQLRvc2AHmxiuuMnF9lbpzaFaE_EoZ5fYxSyoWpubL-Kk1_FolYHEwgvINN6Y21B4bNjA2xgnoMrWfe1ySyQrOgIPyL2sa-BKqR7FRFQjwQi0DYVme62rYZHTjK8aWgh4tJ3w1hDF4Sq5JF2m7Uvs1L9ceQYwuL3fsHIev5KP1DxaJ6VyIrVITYGSrwmlORcvRHy3eKGrD_WHXvivso5jxHKDbaO8qDmBa7BkA3dUX7jIGwuBR_katCzFHFG-qiMQ-JZCVa6DyACsvMYVabFqYXkR8mB0aT5G4zml-xuQb5DfnJwX9RdQgm9bc2273NxAcXdHLsLkh-Y9C4xvZPlSPNXsAqyYj8OmozVMjPw0z2LxTFyaZ45WLwIjDHv0GNnG8-N8getrCXPUEvclTAIOkHwxjxPhZSwPhERpLm3_Dvp_4071vwpDO5zrvs57pVEPzUWV3LVhDptuVRJPhtTUhqKlxz9yJxqTqk_dxy7wxdJxMOnVVy6lfV_wzRTh1mlzDTVJAvo_5quTrVknknXyDXYkvkvuSoT6x3hrNm8xtX3sCtQyymOVx6TVZAZFhdERB6Qcv710dG5tiBlCMUn4R7-lDMHSm-O0s_j3_0jLhF551rL_mUmVecu-gKBxfUe6u_S3pBu6pS8TXiNwSsasqgEoQG1zmtozGV4o5OdrWzggzZwbRchwNfskzTgyZTzDlU3MsMgfFFDUhUQ3QIEGhrtDrlurnEPeLEtb8Evl7JcAnS4XTFrqdS2MSFm7i3q3puleA8vu64fKMk4phFTRLR6Z404L0P3JUEhh1Svi37kPyk-aXTk_0vEwgIxKtFUEFIzskMj0usPSOgFP8liaD_LGrC4_0EkB8vyRCk8ZpA2PfIj23H_ykHX75sFJfRiew9zWLQZulowFcoVS-YXIxN7mg6tawRJ_5hA36Ve4iey6sRo1L6fDd7V_9fNiw8UiFF9VJl2LrydYhSWbGfEKFQ-BZ0BicL9rGk4i237FesLs6LZkluMGa6tplYhm87tD1Zh8sREzQW3zwAJKO6WqW2aYamvEJ6bNarpU8Y_CG-5a3lxNrBCXmZRvXQZSDNYSbNo1AaaBqYKcc-CSon8Q5dQDIhaHpLSSh1QJ8T-hznkH-393YHkCikl1kmz6VhT4iqb9MLuFOdlvJnFslqQsezeWVZ1pwPaBakvhGYOKd5KXTMRmqchg1u-w6_htfmqjbzK30qQ4wwT5ehExXlDxyf7N5LBFY-YysFrCPTo9vCG8f7vHIaKWnYS4HlUIdImzLiC_4O6NDHyYO6zOUUp6Q0Ij_xEzA6ExijkAcANvcp9tt6fS2LSoZfsKn6gcd4YoMWiHyOzN8mLCLLvvwHeaCmd2aLw8UesgvOL38_xujD3I1avbKDl_9ISajRON00D5Dh9rqSYZSsgcYKJlggNxQvUdlYqCByLh_FlhftQXKC7yqs3FLweG32FWhIfcWzARjvEzPGFfQYZlgPwfwdqucbSV_iXaF1SXzcZ3mKiN1qfmBqVbCTEaXIaB5NSVK68n5HzOn74_1qnBtBojwlYjQpySOKWUTMsFrh4SNZcI1_D_46ov79A_mS0.svg)
## Video
[Link](https://youtu.be/onnLPy-Vgns)

## flow chart
[Link](https://online.visual-paradigm.com/community/share/untitled-diagram-ingw42k5)
