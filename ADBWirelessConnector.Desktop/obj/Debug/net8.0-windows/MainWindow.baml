   M S B A M L   `   `   `  ����W  SADBWirelessConnector.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:ADBWirelessConnector.DesktopADBWirelessConnector.Desktop  5   	   -    'ADBWirelessConnector.Desktop.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:ADBWirelessConnector.Desktop     5   	     � Title$  Wireless Phone Connector��5   	   $	��300��6*   $	��400��67    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ CanMinimize=�5
   	    � Icon$ resources/logo.png��6"    � 	Resources 5      7   1ADBWirelessConnector.Desktop.EnumToBrushConverter   EnumToBrushConverter��%   &	          5   
   6%   5      .��5      �   
Background#���  6   .+�5   
   ��  �� Padding$ 3q�6   $
��40 5q�6    �� CornerRadius$ 3��6+   $	��Top=�6�   ��6<   �� *Status+ �� 	Converter# [  6�   �� 5   
   $��20 10q�6   .+�5      �� .+�5      �� $��ADB path��6    	 Label#���  6,   � 5       � ColumnDefinitions 5      �� 5      $��* �6+   �� 5      $	��100 �6+   .+�5      �� -   5        	 ��Name$
	 path��5   .   $��0��6    	 Entry#���  6<   �� 5      -   5        
 ��Name$
 browseButton��5   -   $��1��6   $��	Browse...��6C     SecondaryButton#���  6h   5       5!      �� 5#      .+�5$      �� $��Phone local address��6   #���  67   �� 5%      -   5        $	 address��5%      #���  6+   5&      �� 5(      .+�5)      �� $
��Port��6   #���  6(   �� 5*      -   5        $
	 port��5*      #���  6(   5+      � 5-      $
��0 20q�6    5.      �� 5/      $��* �6'   �� 50      $��* �6'   .+�53      �� -   5        $
 
disconnect��53   )   $��0��6   $��
Disconnect��6=   #���  6g   ${�False��6�   �� 54      -   5        $
 connect��54   )   $��1��6   $��Connect��6:     PrimaryButton#���  6^   55      57      59      5:      