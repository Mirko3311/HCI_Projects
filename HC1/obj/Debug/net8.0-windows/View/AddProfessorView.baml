   M S B A M L   `   `   `  ����R  NPrviProjektniZadatakHCI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:PrviProjektniZadatakHCI.ViewPrviProjektniZadatakHCI.View  5      K*clr-namespace:PrviProjektniZadatakHCI.UtilPrviProjektniZadatakHCI.Util  5      3    -PrviProjektniZadatakHCI.View.AddProfessorView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:PrviProjektniZadatakHCI.View     5      =local1*clr-namespace:PrviProjektniZadatakHCI.Util     5      -   5          � 	Resources  5      <   6PrviProjektniZadatakHCI.Util.BoolToVisibilityConverter   BoolToVisibilityConverterC   =PrviProjektniZadatakHCI.Util.InverseBoolToVisibilityConverter $  InverseBoolToVisibilityConverter��%:   &	        &	        5   
   6+     5   
   62   5      .��5      �� $
��Auto=�6   .��5   
   � $��20q�6    � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5      �� $��0��6   
�������
�������$��10q�5        �� CornerRadius$ 15��5!      $��10,10,20,10q�5"       �� Padding$ 15q�5#       J� Effect 5$      3  -System.Windows.Media.Effects.DropShadowEffect  5%         Color$ #888888��6+      
BlurRadius$ 10Q�6;      ShadowDepth$ 2Q�6K   ._�5'      �� $��10q�6!   .+�5(      ��   NameText#���  6$     DynamicTextBlockStyle#���  6F   $	��0,5q�6v   �� 5)      $��0,0,0,10q�6D   ��6"   �� *NewProfessor.ime+6D   �� 5+        SurnameText#���  6$   #���  6I   $	��0,5q�6y   �� 5,      $��0,0,0,10q�6H   ��6"   �� *NewProfessor.prezime+6H   �� 5.        	EmailText#���  6$   #���  6G   $	��0,5q�6w   �� 50      $��0,0,0,5q�6t   ��6"   �� *ProfessorEmail+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�6t   �� 51        InvalidEmailFormat#���  6$   
���  ��$��12(�53      $��0,0,0,10q�55      y�54      �� *EmailValidationVisibility+55      �� 56        AcademicTitleText#���  6$   #���  6O   $	��0,5q�6   �� 57      $��0,0,0,10q�6G   ��6"   �� *NewProfessor.Zvanje+6G   58      59      �� 5;      $��1��6   
�������
�������$��10q�5>      $ 15��5?      $��
10,10,0,10q�5@      $ 15q�5A       5B        5C      $ #888888��6+   $ 10Q�6;   $ 2Q�6K   ._�5E      �� $��10q�6!   .+�5F      ��   UsernameText#���  6$   #���  6J   $	��0,5q�6z   �� 5G      $��0,0,0,10q�6I   ��6"   �� *NewProfessor.username+6I   �� 5H       	 PasswordText#��� 	 6$   #���  5I      $	��0,5q�5J      � 5K      $��0,0,0,10q�6    5L      �� 5M   "   $��* �63   �� 5N   "   $
��Auto �63   .+�5P      G� -   5         G�Name$ passwordBox��5P   *   $��0��5Q      y�5S      �� *IsPasswordVisible+ �� 	Converter# [ �� 5T      $��0��6&   ��5U      �� *
Password+$	 TwoWay=�$
 PropertyChanged=�y�5V      �� *IsPasswordVisible+# [  d� 5X      $��1��6+   $��40��5[      $
��Auto��5\      $��5,0,0,0q�5]      $��Center=�5^       � ToolTip$ Show/Hide password��5_      ��5Y      �� *
ToggleIcon+ d� 	IsChecked 5Z      �� *IsPasswordVisible+$	 TwoWay=�5_      5`      �� 5a       

 IdText#��� 
 6$   #���  6D   $	��0,5q�6t   �� 5b      -   5         ��Name$ id��5b   "   $��0,0,0,5q�5d   
   ��5c   
   �� *ProfessorId+$	 TwoWay=�$
 PropertyChanged=�5d   
   �� 5e        IdIntegerValue#���  6$   
���  ��$��12(�5g      $��0,0,0,10q�5i      y�5h      �� *IdValidationVisibility+5i      5j      5k      5l      �� 5o      $��1��6     	AddButton#���  5p        CustomButtonStyle#���  5q      $��Center=�5s      $��	0,20,0,10q�5t      $	��120��5u      $��50��5v      ��5r      �� *AddProfessorCommand+5v      5w      5x      5y      