   M S B A M L   `   `   `  ����H  DUI_QueenAnnes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:UI_QueenAnnes.ShipmentUI_QueenAnnes.Shipment  5   	   -    'UI_QueenAnnes.Shipment.ShipmentReceived   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:UI_QueenAnnes.Shipment     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5           MaterialDesignBody#���   5	   	     MaterialDesignPaper#���  5
   	   $��Light"�5   	   $��14(�5   	     � Title$  ShipmentReceived��5   	   $	��450��6"   $	��650��6/    � WindowStartupLocation$ CenterScreen=�6R    � WindowStyle$
 None=�6w   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      � $��Center=�6   $��Center=�6(    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   ���	#FF03A9F4$	��600��6R   .+�5      �� $��Curiosity Shop:��6   $��25(�6.   
�������$
��Bold"�6O   $	��5,3q�6a   �� 5      $��Receive Shipment Details��6   $��25(�68   
�������$	��5,3q�6Y   5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5!      � $��0��6    5"      �� 5#      $
��Auto �6$   �� 5$      $
��Auto �6$   �� 5%      $
��Auto �6$   .+�5(      � $��0��6    5)      �� 5*      $
��Auto �6(   �� 5+      $
��Auto �6(   �� 5,      $
��Auto �6(   .+�5.      �� -   5         ��Name$ LblNoReceiveStatus��5.       $��0��6<   $��30��6I   $��5q�6U    �� Padding$	 5,3q�6`   ��6n   ��  �� Path$ ReceiveStatus��� 5/      $��1��6!   $ 
Horizontal=�6.   $��5q�6G   .+�50      �� $��ShipmentId: ��6    $��5q�67   $	��5,3q�6B   �� 51      -   5        	 ��Name$	 LblShipmentId��51       $��5q�6[   $	��5,3q�6f   ��67   �� $ 
ShipmentId�6f   52      �� 53      $��2��6!   $ 
Horizontal=�6.   $��5q�6G   .+�54      �� $��Unit Name: ��6    $��5q�66   $	��5,3q�6A   �� 55      -   5        $	 LblUnitName��55       $��5q�65   $	��5,3q�6@   ��6N   �� $ UnitName�56      57      � 59      -   5        
 �Name$
 LblHasReceive��59   $   $��1��6   $��0,20,0,0q�6;    5:      �� 5;      $
��Auto �6(   �� 5<      $
��Auto �6(   �� 5=      $
��Auto �6(   �� 5>      $
��Auto �6(   �� 5?      $
��Auto �6(   .+�5B      �� $��0��6!   $ 
Horizontal=�6.   $��5q�6G   .+�5C      �� $��Arrival Date:��6    $��5q�68   $	��5,3q�6C   �� 5D      -   5        $	 	LblArrive��5D       $	��150��6�   $��5q�6�   $	��5,3q�6�   ��63   �� $  SelectedReceive.ArivalDate� �� StringFormat$ {0:dd-MMM-yyyy}��6�   5E      �� 5G      $��1��6!   $ 
Horizontal=�6.   $��5q�6G   .+�5H      �� $��Item Condition:��6    $��5q�6:   $	��5,3q�6E   �� 5I      -   5        $	 LblCondition��5I       $	��150��6i   $��5q�6u   $	��5,3q�6�   ��66   �� $ SelectedReceive.Condition�6�   5J      �� 5L      $��2��6!   $ 
Horizontal=�6.   $��5q�6G   .+�5M      �� $��Employee Received:��6    $��5q�6=   $	��5,3q�6H   �� 5N      -   5        $	 LblEmployee��5N       $	��150��6i   $��5q�6u   $	��5,3q�6�   ��65   �� $  SelectedReceive.PersonName�6�   5O      5P      �� 5R      $��2��6   $ 
Horizontal=�6*   $��Center=�6C   $��0,15,0,0q�6`   .+�5S      �� -	   5         ��Name$ BtnAdd��5S      $��40��6-   $��10q�68   $��5q�6Y   $y�	Collapsed=�6e   .��5T      �� $��/Item/Add.png��6    5U      �� 5W      -
   5        $ BtnBack��5W      $��40��6.   $��10q�69   $��5q�6E   .��5X      �� $��/Item/Back.png��6    5Y      5Z      5[      � 5]      -   5        $
 GrdAddReceive��5]   #   $��1��6   $��Center=�6:   $��Center=�6W   $y�	Collapsed=�6r    5^      �� 5_      $
��Auto �6$   �� 5`      $
��Auto �6$   �� 5a      $
��Auto �6$   �� 5b      $
��Auto �6$   .+�5e      �� $��0��6   $ 
Horizontal=�6*   $��5q�6C   .+�5f      �� $��Arrival Date:��6   $��5q�64   $	��5,3q�6?   (  "System.Windows.Controls.DatePicker  5g      -   5          Name$ DpNewArrive��5g   !   $��5q�66   $	��5,3q�6A   $	��150��6O   5h      �� 5j      $��1��6   $ 
Horizontal=�6*   $��5q�6C   .+�5k      �� $��Item Condition:��6   $��5q�66   $	��5,3q�6A   �� 5l      -   5         ��Name$ CmbNewCondition��5l      $��5q�68   $	��5,3q�6C   $	��150��6Q   ��5m      �� $ ConditionList� �� SelectedItem 5n      �� $ SelectedCondition�5o      �� 5q      $��2��6   $ 
Horizontal=�6*   $��5q�6C   .+�5r      �� $��Employee Received:��6   $��5q�69   $	��5,3q�6D   �� 5s      -   5        $ CmbNewEmployee��5s      $	��150��67   $��5q�6C   $	��5,3q�6N   ��5t      �� $ EmployeeList� 5u      �� $ SelectedEmployee�5v      �� 5x      $��3��6   $ 
Horizontal=�6*   $��Center=�6C   $��20q�6`   .+�5y      �� -   5        $ BtnAddEmployee��5y      $��50��65   $��5q�6@   .��5z      �� $��/Person/Save.png��6    5{      5|      5}      5~      5�      5�      