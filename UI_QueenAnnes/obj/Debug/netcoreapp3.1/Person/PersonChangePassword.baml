   M S B A M L   `   `   `  ����H  DUI_QueenAnnes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:UI_QueenAnnes.PersonUI_QueenAnnes.Person  5   	   /    )UI_QueenAnnes.Person.PersonChangePassword   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:UI_QueenAnnes.Person     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	     MaterialDesignPaper#���  5
   	   $��Light"�5   	   $��14(�5   	     � Title$  PersonChangePassword��5   	   $	��280��6&   $	��350��63    � WindowStartupLocation$ CenterScreen=�6?    � WindowStyle$
 None=�6d   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      � $��Center=�6   $��Center=�6(   $��10q�6C    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   ���	#FF03A9F4.+�5      �� $��Curiosity Shop:��6   $��20(�6.   
�������$
��Bold"�6O   $	��5,3q�6a   �� 5      -   5         ��Name$ 	LblHeader��5      $��Profile��6'   $��20(�6:   
�������$	��5,3q�6[   5      �� 5   
   $��1��6   $ 
Horizontal=�6"   $
��10,5q�6;   .+�5      �� $��Name: ��6   $	��5,3q�6%   �� 5       $��10,0,0,0q�64   $	��5,3q�6F   $	��150��6T   ��6   ��  �� Path$ MyName�6T   5!      �� 5#   
   $��2��6   $ 
Horizontal=�6"   $
��10,5q�6;   .+�5$      �� $��Current Email: ��6   $	��5,3q�6.   �� 5%      $��10,0,0,0q�67   $	��5,3q�6I   $	��175��6W   ��6   �� $ MyEmail�6W   5&      �� 5(   
   $��3��6   $ 
Horizontal=�6"   $
��10,5q�6;   .+�5)      �� $��Current Password: ��6   $	��5,3q�61   �� 5*      -   5         ��Name$ TxtPastPassword��5*      $��10,0,0,0q�6/   $	��5,3q�6A   $	��150��6O   5+      �� 5-   
   $��4��6   $ 
Horizontal=�6"   $
��10,5q�6;   .+�5.      �� $��New Password: ��6   $	��5,3q�6-   �� 5/      -   5        $ TxtNewPassword��5/      $��10,0,0,0q�6.   $	��150��6@   $	��5,3q�6L   50      �� 52   
   $��5��6   $ 
Horizontal=�6"   $��Center=�6;   $��0,20,0,0q�6X   .+�53      �� -   5         ��Name$ BtnSave��53      $��50��6&   $��30��61   $��5q�6S   	 � ToolTip$
	 Save��6_   .��54      �� $��/Person/Save.png��6   55      �� 56      -   5        $ BtnBack��56      $��50��6&   $��30��61   $��5q�6=   $
	 Back��6I   .��57      �� $��/Item/Back.png��6   58      59      5:      5;      