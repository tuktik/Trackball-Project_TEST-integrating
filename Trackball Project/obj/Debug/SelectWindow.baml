   M S B A M L   `   `   `  ����  Trackball Project$    Trackball_Project.SelectWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   T PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	     � Title$  데굴데굴��5   	   (  "MahApps.Metro.Controls.MetroWindow   TitlebarHeight$ 50��6      TitleForeground
 �����$	��300��5   	   $	��380��6   
������� � 
ResizeMode$ CanMinimize=�5   	    � WindowStartupLocation$ CenterScreen=�6"    J� OpacityMask
 �   � � Icon$ icon.png��6[      EnableDWMDropShadow .    WindowTitleBrush �#5872A5.��5	      � .+�5   
   �� -   5        $��MusicButtonFrame��5      $��62��6(   $��62��63   $��/Image/Empty.png��6?   $��25,45,209,217q�6Y   �� 5   
   -   5        $��VideoButtonFrame��5      $��62��6(   $��62��63   $��/Image/Empty.png��6?   $��120,45,114,217q�6Y   �� 5   
   -   5        $��PictureButtonFrame��5      $��62��6*   $��62��65   $��/Image/Empty.png��6A   $��210,45,24,217q�6[   �� 5   
   -   5        $��DesktopButtonFrame��5      $��62��6*   $��62��65   $��/Image/Empty.png��6A   $��25,170,209,92q�6[   �� 5   
   -   5        $��CustomButtonFrame��5      $��62��6)   $��62��64   $��/Image/Empty.png��6@   $��120,170,114,92q�6Z   �� 5   
   -   5        $��MusicButtonObject��5      $��60��6*   $��60��65   $��25,45,209,217q�6U   ��5      �� 5      .R�5      � .+�5      �� $��MusicButton��6    $��/Image/Music.png��63   5      	 �� Triggers	 5      P� 5      
 P� Property
 �@J�IsMouseOver P� Value . . �5      ��  �� 
TargetName$ MusicButton��67    �� Property � �� �� Value$ /Image/Music Click.png��6P   �� 5       �@�Cursor$
 Hand��67   5      5      5      �� 5"   
   -   5        $��VideoButtonObject��5"      $��60��6*   $��60��65   $��120,45,114,217q�6U   ��5#      �� 5$      .R�5%      � .+�5&      �� $��VideoButton��6    $��/Image/Video.png��63   5'      	 5(      P� 5)      
 �@J�IsMouseOver . . �5*      �� $ VideoButton��67    � ��$ /Image/Video Click.png��6P   �� 5+       �@�Cursor$
 Hand��67   5,      5.      50      �� 53   
   -   5        $��PictureButtonObject��53      $��60��6,   $��60��67   $��210,45,24,217q�6Y   ��54      �� 55      .R�56      � .+�57      �� $��PictureButton��6    $��/Image/Picture.png��65   58      	 59      P� 5:      
 �@J�IsMouseOver . . �5;      �� $ PictureButton��67    � ��$ /Image/Picture Click.png��6R   �� 5<       �@�Cursor$
 Hand��67   5=      5?      5A      �� 5D   
   -	   5        $��DesktopButtonObject��5D      $��60��6,   $��60��67   $��25,170,209,92q�6Y   ��5E      �� 5F      .R�5G      � .+�5H      �� $��DesktopButton��6    $��/Image/Desktop.png��65   5I      	 5J      P� 5K      
 �@J�IsMouseOver . . �5L      �� $ DesktopButton��67    � ��$ /Image/Desktop Click.png��6R   �� 5M       �@�Cursor$
 Hand��67   5N      5P      5R      �� 5U   
   -
   5        $��CustomButtonObject��5U      $��60��6+   $��60��66   $��120,170,114,92q�6W   ��5V      �� 5W      .R�5X      � .+�5Y      �� $��CustomButton��6    $��/Image/Custom.png��64   5Z      	 5[      P� 5\      
 �@J�IsMouseOver . . �5]      �� $ CustomButton��67    � ��$ /Image/Custom Click.png��6Q   �� 5^       �@�Cursor$
 Hand��67   5_      5a      5c      �� 5f   
   -   5        $��60��5f      $��60��6   $��210,170,24,92q�6;   ��5g      �� 5h      .R�5i      � .+�5j      �� $��
NoneButton��6    $��/Image/None.png��62   5k      	 5l      P� 5m      
 �@J�IsMouseOver . . �5n      �� $ 
NoneButton��67    � ��$ /Image/None Click.png��6O   �� 5o       �@�Cursor$
 Hand��67   5p      5r      5t      5u      5v      