   M S B A M L   `   `   `  ����H  DUI_QueenAnnes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:UI_QueenAnnes.ItemUI_QueenAnnes.Item  5   	        UI_QueenAnnes.Item.AddUnit   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:UI_QueenAnnes.Item     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	     MaterialDesignPaper#���  5
   	   $��Light"�5   	   $��14(�5   	     � Title$$  Curiosity Store - Add New Unit��5   	   $	��370��60   $	��700��6=    � WindowStartupLocation$ CenterScreen=�6I    � WindowStyle$
 None=�6n   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      � $��Center=�6   $��Center=�6(    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   ���	#FF03A9F4$	��650��6R   .+�5      �� $��Curiosity Shop:��6   $��25(�6.   
�������$
��Bold"�6O   $	��5,3q�6a   �� 5      $��Item Details��6   $��25(�6,   
�������$	��5,3q�6M   5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5!      � $��0��6   $��20q�6#   $��Center=�6/   $��Center=�6L    5"      �� 5#      $
��Auto �6(   �� 5$      $
��Auto �6(   �� 5%      $
��Auto �6(   �� 5&      $
��Auto �6(   �� 5'      $
��Auto �6(   .+�5*      � $��0��6   $��0,10,0,0q�6(    5+      �� 5,      $
��Auto �6/   �� 5-      $
��Auto �6/   .+�5/      �� $��0��6    $��Price:��60   $��5q�6A   $	��5,3q�6L   �� 50      -   5         ��Name$ TxtPrice��50   "   $��1��64   $��5q�6D   $	��5,3q�6O   $	��150��6]   ��6i   ��  �� Path$ SelectedUnit.UnitPrice�51      � 53      $��1��6   $��0,10,0,0q�6(    54      �� 55      $
��Auto �6/   �� 56      $
��Auto �6/   .+�58      �� $��0��6    $��Description:��60   $��5q�6G   $	��5,3q�6R   �� 59      -   5        $ TxtDesc��59   "   $��1��63   $	��200��6C   $��5q�6O   $	��5,3q�6Z   ��6h   �� $" SelectedUnit.UnitDescription�5:      � 5<      $��2��6   $��0,20,0,0q�6(    5=      �� 5>      $
��Auto �6,   �� 5?      $
��Auto �6,   �� 5@      $
��Auto �6,   .+�5C      � $��0��6    5D      �� 5E   "   $
��Auto �63   �� 5F   "   $
��Auto �63   .+�5I      �� $��0��6$   $��Category Name:��64   �� 5J      -   5         ��Name$ 
CmbCatName��5J   '   $��1��6;   $��10,0,0,0q�6K   $	��150��6]   ��5K      �� $ CategoryLists�	 �� SelectedItem	 5L      �� $ SelectedCat�5M      �� 5O      $��2��6%   $ 
Horizontal=�62   $��Center=�6K   .+�5P      �� -   5        
 ��Name$
 BtnViewCatName��5P   %   $��50��6=   $��5q�6H   $��0,20,0,0q�6T    � ToolTip$ View Category Details��6�   .��5Q   "   �� $��/Person/Review.png��6(   5R      5S      5U      5V      � 5Y      -   5         �Name$ GrdCategory��5Y   8   $��1��6   $��50,10,10,10q�6#   $y�	Collapsed=�6M    5Z      �� 5[      $
��Auto �6$   �� 5\      $
��Auto �6$   �� 5]      $
��Auto �6$   .+�5_      �� $��0��6   $��- Category Details -��6%   $��DemiBold"�6D   $��16(�6Z   $��Italic$�6h   �� 5a      $��1��6   $��
0,10,10,10q�6*   $��Center=�6>   .+�5b      �� $��Category Name: ��6   $��0,5,5,5q�66   $	��5,3q�6G   �� 5c      -   5        $ TxtCategoryName��5c      $��20,5,5,5q�67   $	��5,3q�6I   $	��150��6W   ${�False��6�   ��6c   �� $ SelectedCat�6�   5d      �� 5f      $��2��6   $��0,20,0,0q�6*   $��Center=�6<   $ 
Horizontal=�6Y   .+�5g      �� -   5        $
 	BtnCatAdd��5g      $��50��60   $��5q�6;   $ Add new Category��6_   .��5h      �� $��/Item/Add.png��6    5i      �� 5k      -   5        $
 
BtnCatEdit��5k      $��50��61   $��5q�6<   $ Edit Selected Category��6a   .��5l      �� $��/Item/Edit.png��6    5m      �� 5o      -	   5        $
 BtnCatDelete��5o      $��50��63   $��5q�6>   $ Delete selected Category��6e   .��5p      �� $��/Item/Delete.png��6    5q      �� 5s      -
   5        $
 
BtnCatSave��5s      $��50��61   $��5q�6<   $ Save Changes��6a   $y�	Collapsed=�6x   .��5t      �� $��/Person/Save.png��6    5u      �� 5w      -   5        $
 
BtnCatBack��5w      $��50��61   $��5q�6<   $ Return��6a   .��5x      �� $��/Item/Back.png��6    5y      5z      5{      5|      � 5~   
   $��2��6   $��	0,30,10,0q�6   $��Center=�6/    5      �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   .+�5�      �� -   5        $��0��5�      $��50��6%   $��5q�60   $��0,0,10,0q�6=   .��5�      �� $��/Person/Save.png��6   5�      �� 5�      -   5        $
 BtnBack��5�   =   $��1��6   $��50��6%   $��5q�60   .��5�      �� $��/Item/Back.png��6   5�      5�      5�      5�      