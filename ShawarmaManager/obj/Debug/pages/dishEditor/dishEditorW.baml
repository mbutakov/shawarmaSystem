   M S B A M L   `   `   `  ����J  FShawarmaManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:ShawarmaManager.pages.dishEditor ShawarmaManager.pages.dishEditor  5   	   2    ,ShawarmaManager.pages.dishEditor.dishEditorW   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   :local.clr-namespace:ShawarmaManager.pages.dishEditor  5   	   -   5          �Name$  dishEditorWindow��5	   	    � WindowStyle$
 None=�5
       � Title$ dishEditorW��5   	   $
��1080��6   $
��1920��6+   ���	#FF3A0842.��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      $��4* �6   .+�5   
   �� -   5         ��Name$ 	buttonAdd��5      $��Добавить��6$   $��Center=�67   $��	0,0,0,970q�6T   $��Bottom=�6g   $��80��6�   $	��276��6�    � Cursor$
 Hand��6�   �� 5   
   -   5         ��Name$ nameDish��5      $��Stretch=�6$   $��0,152,0,867q�6B    �� TextWrapping$
 Wrap=�6W   $!��Название блюда��6k   $	��276��6�   $ IBeam��6�   $��Stretch=�6�   $��20(�6�    �� CharacterCasing$ Normal=�6�   $��Normal"�6�   $��Franklin Gothic Medium)�6�   	 �� TextAlignment$	 Center=�6  
�������6.  �� 5   
   -   5        $ 	priceDish��5      $��54,238,54,781q�6%   $
 Wrap=�6<   $��Цена��6P   $	��276��6\   $ IBeam��6h   $��20(�6w   $ Normal=�6�   $��Normal"�6�   $��Franklin Gothic Medium)�6�   $	 Center=�6�   
�������6�   �� 5   
   -   5        
 ��Name$
 ingridientListAdded��5      $��	0,388,0,0q�6[    �� View 5      �� 5      .P�5      ��  �� Width$
 auto��6V    �� DisplayMemberBinding 6%   ��  �� Path$ 
Item1.Name�.��6c   Название6m   �� 5      $
 auto��6Q    6%   �� $ Item2�.��6^   Кол-во6f   5      5      � 5   
   -   5         �Name$ gridIngridient��5      $
��35pxq�6   $��1��65    5      �� 5       $��1* �6#   �� 5!      $��1* �6#   �� 5"      $��1* �6#    � RowDefinitions 5$      �� 5%      $��1* �6    �� 5&      $��1* �6    �� 5'      $��1* �6    .+�5*      �� $��3��6   $��3��6/   $
��Auto=�6@   $
��Auto=�6c   5-      5.      �� 50   
   -   5        $ closeB��50      $��X��6"   $
 Hand��6C   $��1496,0,0,1048q�6Q   $
��Left=�6h   $��40��6�   $��1��6�   �� 51   
   -   5        $ 	deleteIng��51      $��Удалить��6$   $��278,351,10,697q�66   6N   �� 52   
   -	   5        $ uploadButton��52      $��Загрузить ico��6'   $
��Left=�6?   $��
54,351,0,0q�6Z   $	��Top=�6n   $	��138��6�   6�   55      56      