   M S B A M L   `   `   `  ����A  =Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ParserParser  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5   	       Parser.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Parser     5   	   Bcol2clr-namespace:System.Collections;assembly=mscorlib     5   	     � Title$  
MainWindow��5	   	   $	��500��6   $	��700��6)   $	��300��65   $	��700��6E   .��5      �  � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      6   �� 5      $��30 �6    � ColumnDefinitions 5   
   �� 5      $��2* �6   6,   �� 5      $��80 �6   6,   �� 5      $	��190 �6   6-   .+�5   
   �� -   5        $��1��5      $��0��6!   $��
Найти��6.   $��20��6S   $��
10,24,0,24q�6_   $
��Left=�6s   $��60��6�    � ToolTip 5      _� 5      .��6   ?=Всплывающая подсказка для кнопки5      5      �� 5   
   -   5         ��Name$ ListBox1��5      $��1��6%   $��0��62   $��10q�6B   $��3��6N    �� View 5      �� 5      P�5       �� 5!       �� Width$
 Auto��6)    �� CellTemplate 5"      �� 5#   "   .R�5$   &   �� $��50��6F   $��10q�6R   ��6,   �� *imgUrl+6R   5%   #   5'      �� 5(      $��
Товар��6)   $
 Auto��68    5)      �� 5*   "   .R�5+   &   �� $��
TextBlock1��60    �� TextAlignment$
 Left=�6X   $
��auto��6m   ��6B   �� *name+6m   5,   #   5.      �� 5/      $��Цена��6)   $
 Auto��67    50      �� 51   "   .R�52   &   �� $��
TextBlock2��60   $
 Left=�6Y   $	��200��6n   ��6B   �� *price+6n   53   #   55      57      59      �� 5:   
   -   5        $��TextBox1��5:      $��0��6"   $��20��62   $��10,25q�6>   $��Center=�6M   �� 5;   
   -   5        	 ��Name$	 	ComboBox1��5;      
 �� IsTextSearchEnabled
 .   �� 
IsEditable . $��2��6�   $
��Left=�6�   $��10,25q�6�   $��Center=�6�   $	��160��6#  ��6�   �� *list+6#  �� 5<   
   -   5         ��Name$ Label1��5<      $
��Left=�6    $
��10,0q�6;   $��2��6I   $��Center=�6V   $��3��6q   5=      5?      