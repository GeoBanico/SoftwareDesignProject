   M S B A M L   `   `   `  ����H  DUI_QueenAnnes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:UI_QueenAnnes.BoughtStatusUI_QueenAnnes.BoughtStatus  5   	   *    $UI_QueenAnnes.BoughtStatus.AddOnline   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:UI_QueenAnnes.BoughtStatus     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	     MaterialDesignPaper#���  5
   	   $��Light"�5   	   $��14(�5   	     � Title$  	AddOnline��5   	   $	��500��6   $	��800��6(    � WindowStartupLocation$ CenterScreen=�64    � WindowStyle$
 None=�6Y   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      � $��Center=�6   $��Center=�6(    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   ���	#FF03A9F4$	��650��6R   .+�5      �� $��Curiosity Shop:��6   $��25(�6.   
�������$
��Bold"�6O   $	��5,3q�6a   �� 5      $��Online Details��6   $��25(�6.   
�������$	��5,3q�6O   5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5!      � $��0��6    5"      �� 5#      $
��Auto �6$   �� 5$      $
��Auto �6$   �� 5%      $
��Auto �6$   �� 5&      $
��Auto �6$   �� 5'      $
��Auto �6$   .+�5*      �� $��0��6   $ 
Horizontal=�6*   $��20,10q�6C   .+�5+      �� $��Date Bought: ��6   $	��5,3q�64   $��5q�6B   (  "System.Windows.Controls.DatePicker  5,      -   5          Name$ DpDateBought��5,   !   $	��5,3q�67   $��5q�6E   $	��150��6P      SelectedDate 6\   ��  �� Path$ SelectedOnline.DateBought�5-      �� 5/      $��1��6   $ 
Horizontal=�6*   $��20,10q�6C   .+�50      �� $��Arrival Bought: ��6   $	��5,3q�67   $��5q�6E     51      -   5        $ DpArrivalDate��51   !   $	��5,3q�68   $��5q�6F   $	��150��6Q    6]   �� $  SelectedOnline.ArrivalDate�52      �� 54      $��2��6   $ 
Horizontal=�6*   $��20,10q�6C   .+�55      �� $��Total: ��6   $	��5,3q�6.   $��5q�6<   �� 56      -   5        	 ��Name$	 TxtTotal��56      $	��5,3q�60   $��5q�6>   $	��100��6I   ��6U   �� $ SelectedOnline.Total�57      �� 59      $��3��6   $ 
Horizontal=�6*   $��20,10q�6C   .+�5:      �� $��
Quantity: ��6   �� 5;      -   5        $	 TxtQuantity��5;      $	��5,3q�63   $��5q�6A   $��50��6L   ��6W   �� $ SelectedOnline.Quantity�5<      �� 5>      $��4��6   $ Vertical=�6*   $��20,10q�6A   .+�5?      �� $ 
Horizontal=�6!   .+�5@      �� $��From Store: ��6    $	��5,3q�67   $��5q�6E   �� 5A      -   5        
 ��Name$
 CmbStore��5A   #   $	��5,3q�65   $��5q�6C   $	��150��6N   ��5B   #   �� $ 	StoreList� �� SelectedItem 5C   #   �� $ SelectedStore�5D      �� 5E      -   5         ��Name$ btnViewStore��5E      $��10q�63   $��50��6?   $��30��6J   $��5q�6V    � ToolTip$ View Online store��6}   .��5F      �� $��/Person/Review.png��6    5G      5H      5I      � 5K      -   5         �Name$ GrdOnlineStore��5K   #   $��1��6   $y�	Collapsed=�6;    5L      �� 5M      $
��Auto �6$   �� 5N      $
��Auto �6$   .+�5Q      �� $��0��6   $��- Online Store Details -��6%   $��Center=�6H   $��DemiBold"�6e   $��16(�6{   $��Italic$�6�   $��10q�6�   � 5S      $��1��6    5T      �� 5U      $
��Auto �6(   �� 5V      $
��Auto �6(   .+�5Y      �� $��0��6!   $��Center=�6.   $��Center=�6K   .+�5Z      �� $��Store Name: ��6    $��-50,10,10,10q�67   �� 5[      -   5        $	 TxtStoreName��5[   "   $	��150��68   ${�False��6h   ��6D   �� $ SelectedStore�6h   5\      �� 5^      $��1��6!   $��0,40,0,0q�6.   $��Center=�6@   $ 
Horizontal=�6]   .+�5_      �� -	   5        $ BtnOnlineStoreAdd��5_   !   $��50��6<   $��5q�6G   $ Add new Online store��6s   .��5`      �� $��/Item/Add.png��6$   5a      �� 5c      -
   5        $ BtnOnlineStoreEdit��5c   !   $��50��6=   $��5q�6H   $  Edit selected Online store��6u   .��5d      �� $��/Item/Edit.png��6$   5e      �� 5g      -   5        $ BtnOnlineStoreDelete��5g   !   $��50��6?   $��5q�6J   $" Delete selected Online Store��6y   .��5h      �� $��/Item/Delete.png��6$   5i      �� 5k      -   5        $ BtnOnlineStoreSave��5k   !   $��50��6=   $��5q�6H   $ Save Changes��6u   $y�	Collapsed=�6�   .��5l      �� $��/Person/Save.png��6$   5m      �� 5o      -   5        $ BtnOnlineStoreBack��5o   !   $��50��6=   $��5q�6H   $ Return��6u   .��5p      �� $��/Item/Back.png��6$   5q      5r      5s      5t      5u      �� 5w   
   $��2��6   $ 
Horizontal=�6"   $��Center=�6;   $��Bottom=�6X   $��10q�6s   .+�5x      �� -   5        $ BtnSave��5x   8   $��50��6   $��35��6    $��5q�6,   $ Save Online Details��6_   .��5y      �� $��/Person/Save.png��6   5z      �� 5|      -   5        $ BtnBack��5|   8   $��50��6   $��35��6    $��5q�6,   $
 Back��6_   .��5}      �� $��/Item/Back.png��6   5~      5      5�      5�      