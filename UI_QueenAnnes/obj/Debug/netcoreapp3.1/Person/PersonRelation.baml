   M S B A M L   `   `   `  ����H  DUI_QueenAnnes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:UI_QueenAnnes.PersonUI_QueenAnnes.Person  5   	   )    #UI_QueenAnnes.Person.PersonRelation   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:UI_QueenAnnes.Person     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	     MaterialDesignPaper#���  5
   	   $��Light"�5   	   $��14(�5   	     � Title$&   Curiosity Shop - Person Relation��5   	   $	��500��62   $	��650��6?    � WindowStartupLocation$ CenterScreen=�6K    � WindowStyle$
 None=�6p   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      � $��Center=�6   $��Center=�6&    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   ���	#FF03A9F4$	��600��6R   .+�5      �� $��Curiosity Shop:��6   $��25(�6.   
�������$
��Bold"�6O   $	��5,3q�6a   �� 5      $��Relation��6   $��25(�6(   
�������$	��5,3q�6I   5      �� 5   
   $��1��6   $ 
Horizontal=�6"   $��10q�6;   .+�5      �� $��Person Name:��6   $��DemiBold"�6+   $	��5,3q�6A   �� 5      $��DemiBold"�66   $	��5,3q�6L   ���	#FF03A9F4��6   ��  �� Path$ MyName�6Z   �� 5      $��|��6   $	��5,3q�6    $��20,0,0,0q�6.   �� 5      $��Access Type:��6   $��DemiBold"�6+   $��20,0,0,0q�6A   $	��5,3q�6S   �� 5       $��DemiBold"�68   $	��5,3q�6N   ���	#FF03A9F4��6   �� $ MyAccess�6\   5!      � 5#   
   $��2��6    � ColumnDefinitions 5$      �� 5%      $��* �6#   �� 5&      $
��0.3* �6#   �� 5'      $��* �6#   .+�5*      � $��0��6   $��20q�6#    5+      �� 5,      $
��Auto �6$   �� 5-      $
��Auto �6$   �� 5.      $
��Auto �6$   �� 5/      $
��Auto �6$   .+�52      �� $��- All Persons -��6   $
��Left=�62   $��Italic$�6M   $��DemiBold"�6`   $��16(�6v   �� 54      $��1��6   $ 
Horizontal=�6*   $��Center=�6C   $��	0,10,0,20q�6`   .+�55      �� -   5         ��Name$ 	LblSearch��55      $��Search��6/   $	��5,3q�6@   �� 56      -   5         ��Name$ 	TxtSearch��56      $	��150��61   $��	-40,0,0,0q�6a   $	��5,3q�6t   �� 57      -   5        	 ��Name$	 	BtnSearch��57      $��35��60   $��20,0,0,0q�6;   $��5q�6M   .��58      �� $��/Item/search_icon.png��6    59      5:      �� 5<      $��2��6   $	��200��6'   $	��250��63   $��0,10,0,0q�6@   ���	#FF03A9F4��5=      �� $ 
PersonList�
 �� SelectedItem
 5>      �� $ SelectedPerson���5?      �� 5@      .R�5A      �� ._�5B   "   �� $ 
Horizontal=�6-   .+�5C   &   �� $	��5,3q�5D   )    �� Padding$	 5,3q�66   $
��Bold"�5E   )   
���������5F   )   �� $ 	FirstName��� 5G   &   $��0,3,5,3q�5H   )   $	 5,3q�6:   $
��Bold"�5I   )   
���������5J   )   �� $ LastName�5K   #   5L      5M      5O      5P      �� 5R      $��1��6   $��Center=�6)   $��
20,80,20,0q�6D   ���	#FF03A9F4.+�5S      �� -   5        $	 BtnAdd��5S      $��40��6)   $��5q�64   $��5q�6U    � ToolTip$  Add Person to Relationship��6`   .��5T      �� $��/Person/next.png��6   5U      �� 5V      -   5        $	 	BtnDelete��5V      $��40��6,   $��5q�67   $��5q�6[   $% Delete Person from Relationship��6f   .��5W      �� $��/Person/back1.png��6   5X      �� 5Y      -   5        $	 BtnBack��5Y      $��40��6*   $��5q�65   $��5q�6W   $
 Back��6b   .��5Z      �� $��/Item/Back.png��6   5[      5\      � 5^      $��2��6   $��20q�6#    5_      �� 5`      $
��Auto �6$   �� 5a      $
��Auto �6$   �� 5b      $
��Auto �6$   .+�5e      �� $��- Friend List -��6   $��0��62   $��DemiBold"�6?   $��16(�6U   � 5g      $��1��6    5h      �� 5i      $
��Auto �6(   �� 5j      $
��Auto �6(   .+�5m      �� $��0��6!   $ Vertical=�6.   $��20,10q�6E   .+�5n      �� $��Selected Name:��6    $	��5,3q�69   $��14(�6G   �� 5o      -   5        $ LblSelectName��5o       $��20��6i   $	��150��6u   $	��5,3q�6�   ��67   �� $ SelectedFriend.FirstName�6�   5p      5q      �� 5t      $��2��6   $	��200��6'   $	��250��63   $��0,10,0,0q�6@   ���	#FF03A9F4��5u      �� $ FriendsList�
 5v      �� $ SelectedFriend���5w      �� 5x      .R�5y      �� ._�5z   "   �� $ 
Horizontal=�6-   .+�5{   &   �� $	��5,3q�5|   )   $	 5,3q�66   $
��Bold"�5}   )   
���������5~   )   �� $ 	FirstName��� 5   &   $��0,3,5,3q�5�   )   $	 5,3q�6:   $
��Bold"�5�   )   
���������5�   )   �� $ LastName�5�   #   5�      5�      5�      5�      5�      5�      5�      