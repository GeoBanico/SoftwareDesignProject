   M S B A M L   `   `   `  ����H  DUI_QueenAnnes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:UI_QueenAnnesUI_QueenAnnes  5   	       UI_QueenAnnes.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:UI_QueenAnnes     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	     MaterialDesignPaper#���  5
   	   $��Light"�5   	   $��14(�5   	     � Title$  Queen Anne Log-In��5   	   $	��400��6#   $	��450��60    � WindowStartupLocation$ CenterScreen=�6<    � ShowInTaskbar$ False��6a    � WindowStyle$
 None=�6w   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      �  � RowDefinitions 5   
   �� 5      $
��0.6* �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��Center=�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� $'��!/Log-In/CuriosityShop_NewLogo.png��6   $��0��6?   $	��100��6P   $	��100��6]   $��0,10,0,0q�6i   �� 5      $��1��6   $��Center=�6)   $
��Left=�6D    �� Orientation$ Vertical=�5      $��	20,10,0,0q�60   .+�5      �� $��	Log in to��6   $��	ExtraBold"�5       $��35(�6/   �� 5!      $��Curiosity Shop��6   $��	ExtraBold"�5"      $��35(�6/   5#      5'      � 5)   
   $��1��6   $��Center=�6   $��Center=�69    5*      �� 5+      $
��Auto �6    �� 5,      $
��Auto �6    �� 5-      $
��Auto �6    .+�50      � -   5         �Name$ GrdLogIn��50   =   $��0��6   $��Center=�6    $y�	Collapsed=�6O    51      �� 52      $
��Auto �6$   �� 53      $
��Auto �6$   �� 54      $
��Auto �6$   .+�57      �� $ 
Horizontal=�6   $��0��66   $	��240��6C   .+�58      �� $��	Username:��6   $��15(�60   $��90��6>   �� 59      -   5         ��Name$ TxtUsername��59      $	��150��63   $	��5,3q�6?   $	��5,3q�6M   $��15(�6Z   5:      �� 5<      $ 
Horizontal=�6   $��1��66   $	��240��6C   .+�5=      �� $��	Password:��6   $��15(�60   $��90��6>   G� 5>      -   5        	 G�Name$	 TxtPassword��5>   "   $	��150��67   $	��5,3q�6C   $	��5,3q�6Q   $��15(�6^   5?      �� 5A      -   5        
 ��Name$
 	BtnSubmit��5A      $��2��6,   $	��240��69   $��Submit��6F   $��Center=�6W   $	��5,3q�6�   $��5q�6�   
�������6�   5B      �� 5E      -   5        $
 BtnLogIn��5E      $��1��6'   $��Log in��64   $��Center=�5F      $	��5,3q�6G   $��5q�6U   $	��240��6`   
�������$��Black"�5G   (   �� 5I      -   5        $
 
BtnAddUser��5I      $��2��6)   $��Sign up��66   $��Center=�5J      $	��5,3q�6I   $��5q�6W   $	��240��6b   
�������$��Black"�5K   (   5L      5M      5N      