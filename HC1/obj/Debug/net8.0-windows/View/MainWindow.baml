   M S B A M L   `   `   `  ����R  NPrviProjektniZadatakHCI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:PrviProjektniZadatakHCIPrviProjektniZadatakHCI  5   	   K*clr-namespace:PrviProjektniZadatakHCI.UtilPrviProjektniZadatakHCI.Util  6?   vPclr-namespace:PrviProjektniZadatakHCI.Resources;assembly=PrviProjektniZadatakHCI!PrviProjektniZadatakHCI.Resources  5   	       ASystem.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   1local%clr-namespace:PrviProjektniZadatakHCI  5   	   5util*clr-namespace:PrviProjektniZadatakHCI.Util  6?   `	resourcesPclr-namespace:PrviProjektniZadatakHCI.Resources;assembly=PrviProjektniZadatakHCI  5   	     � WindowState$  	Maximized=�5   	   6   0PrviProjektniZadatakHCI.Resources.SharedResource   
LogInTitle � Title# Z 5       � 	Resources 5
      3   -PrviProjektniZadatakHCI.Util.EyeIconConverter   EyeIconConverterC   =PrviProjektniZadatakHCI.Util.InverseBoolToVisibilityConverter $  InverseBoolToVisibilityConverter<   6PrviProjektniZadatakHCI.Util.BoolToVisibilityConverter  BoolToVisibilityConverter��%W   &	        &	      &	 &       5   
   6      5   
   60     5   
   6)   5      .��5      � .+�5   
   �� $��Stretch=�6   $��Stretch=�6-   ��5      �� 5       �� ImageSource$ /Images/school.jpg��6    j� Stretch$ UniformToFill=�6>   5      � 5   
   .+�5      ��  �� Orientation$ Vertical=�6   $
��Left=�60   $��30q�6K   .+�5      �� $��/Images/logo.png��6   $	��300��62   $	��284��6>   $��20q�6K   $��Uniform=�6W   5      �� 5      ���#3aafd0 �� CornerRadius$ 60��6*    �� Padding$ 20q�6<   $	��404��6I   $	��500��6U   $��Center=�6b   $��Center=�6}   ._�5       �� $��0q�6   $	��368��6(   $	��386��64   .+�5!      ��   
LogInTitle#���  6    $
��0 40q�5"       ���#ffffff	 �� TextAlignment$	 Center=�6C   $��38(�5#       $��Arial)�6.   $
��Bold"�6A   �� 5%      $ 
Horizontal=�6!   $��10q�6:   .+�5&      '  !MaterialDesignThemes.Wpf.PackIcon  
   Kind$

 User=�62   $��30��6>   $��30��6I   ���#FFFFFF6U   �� 5'      -   5         ��Name$ txtUsername��5'   "   $
��20 0q�67   $��10q�6F     EnterCourseName)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5(   #   ���#000000$	��230��5)   8   ���#3498DB���#F7F9FC �� 
CaretBrush �#3498DB5*   8   5+      � 5-      $��10q�6    � ColumnDefinitions 5.      �� 5/      $
��Auto �6/   �� 50      $
��Auto �6/   �� 51      $��40 �6/   .+�53        $

 Lock=�62   $��0,0,10,0q�6>   $��30��6P   $��30��6[   ���#FFFFFF$��0��6|   $��Center=�6�   � 55      $��1��6   $
��10 0q�63   .+�56      G� -   5         G�Name$ passwordBox��56   *   $��10q�57      $	��230��58        EnterPassword# �  5;      ���#000000���#3498DB���#F7F9FC G� 
CaretBrush �#3498DBy�59      �� *IsPasswordVisible+ �� 	Converter# [ 5?      �� 5A      $��1��6"   $��10q�5C      $	��230��5D      # �  5G      ���#000000���#3498DB���#F7F9FC �#3498DBy�5E      �� *IsPasswordVisible+# [ ��5F      �� *
Password+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5K      5M      d� 5O      $��2��6'    � ToolTip$ Prikaži/Sakrij lozinku��5Q      $��Center=�5R      
�������$��0q�5T      $��0,0,5,0q�6&   $��30��5U      $��30��6   
������� d� 	IsChecked 5P      �� *IsPasswordVisible+$ TwoWay=�.��5W        $��15��5X   	   $��15��6   
 5Y   	   �� *	IsChecked+ �� RelativeSource ��  �� AncestorType ToggleButton# [  5\      5^      �� 5`      $
��0 30q�6!   $��Center=�6/   .+�5a      �� -   5         ��Name$ btnLogin��5c   !   $	��120��5a   !   $��50��6-   ���#3498DB$��2q�6O   ���#3498DB
�������$��20q�5b   I   $ Login��6U     LogInButton#���  5d   !   $
��Bold"�5e   !   +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 8��63   5f      5g      5h      5i      �� 5j   
   $ 
Horizontal=�6   $��Right=�6.   $��Bottom=�6J   $��20q�6e   .+�5l      �� -   5        $	��100��5l   5   $	��100��6A   
������ 
������ $ Switch to English��6�   .��5m      �� $��Images/EnglishIcon.png��6   $��70��68   $��64��6C   5n      �� 5o      -   5        $	��100��5o   5   $	��100��6A   
������ 
������ $ Switch to Serbian��6�   .��5p      �� $��Images/SerbianIcon.png��6   $��72��68   $��60��6C   5q      5r      5s      5t      