   M S B A M L   `   `   `  ����I  ECardGameClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CardGameClientCardGameClient  5   	        CardGameClient.LoginScreen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   +dcclr-namespace:CardGameClient     5   	   -   5        $��LoginWnd��5   +     � Title$  Avalon: Battles for Glory��6	   $	��768��6;   $
��1024��6K   $	��768��6[   $
��1024��6h    � Cursor$1 +/CardGameClient;component/Images/cursor.cur��5   	    � WindowStyle$
 None=�6>    � WindowStartupLocation$ CenterScreen=�6Q    � 
ResizeMode$ NoResize=�5   	    � WindowState$ 	Maximized=�6   .��5      �  �� BitmapScalingMode$
 Fant=�6   ��5   
   �� 5       �� ImageSource$: 4/CardGameClient;component/Images/Background/bg03.jpg��6    j� Stretch$ UniformToFill=�6\   .+�5   
   � -   5        $��grid1��5   9   $	��230��6   $��Center=�6   $��Center=�6F   $	��395��6a   ��5      �� 5      $5 //CardGameClient;component/Images/login_form.png��6   .+�5      �� -   5        $��LoginErrorInfo��5      $��Center=�6*   $��0, 20, 0, 0q�6G   $	��Top=�6\   
���  ��$�� ��5   %   �� 5      -   5        $��loginTextBox��5   d   $
��Left=�6   $�� ��61   $��
5, 3, 3, 5q�69   $��166,56,0,148q�6N   $	��150��5      
�������$��0��65   $��0q�6B   	 J� SnapsToDevicePixels	 . $��13(�5      
 �� TextAlignment$
 Justify=�6$   $��Tahoma)�6<    �� 
CaretBrush
 �����.��6h   ��5      �� 5      $2 ,/CardGameClient;component/Images/textbox.png��6!   .��5   &   5      G� 5      -   5        $��passwordTextBox��5       $��26��5      $
��Left=�6&   $��
5, 7, 7, 5q�6A   $��
166,96,0,0q�6V   $	��Top=�5    1   $	��150��6I   
�������$��1��6h   $��0q�5!      $��13(�6.   $��Tahoma)�6<    G� PasswordChar$ *��6P    G� 
CaretBrush
 �������5"      �� 5#      $2 ,/CardGameClient;component/Images/textbox.png��6!   5%      �� 5&      -   5        $��
login_text��5'      $
 Fant=�5&      $��25��6;   $
��Left=�6G   $��	88,57,0,0q�6b   $	��Top=�5'   &   $��71��6>   $5��//CardGameClient;component/Images/login_text.png��5(      �� 5)      -   5        $��password_text��5*      $
 Fant=�5)      $��25��6;   $
��Left=�6G   $��	77,97,0,0q�6b   $	��Top=�5*   )   $��82��6A   $8��2/CardGameClient;component/Images/password_text.png��5+         CardGameClient.StyledBtn  5,      -   5          Name$ loginBtn��5,         CardGameClient.StyledBtn   MouseUp$ loginBtn_MouseUp��5-         Text$ 
Войти��5,   -   $��34��6:   $	��130��6F   $��56,156,0,40q�6R   $
��Left=�5-   6     5.      -	   5        $ exitBtn��5.      $ exitBtn_MouseUp��5/   7   $ 
Выйти��5.   ,   $��34��69   $	��130��6E   $��0,156,53,40q�6Q   $��Right=�5/      67   50      � 52   
   -
   5        $��grid2��52   9   $��38��6   $��Stretch=�6   $��Bottom=�6F   ���	#C5000000.+�53      �� -   5        $��	errorText��53      $^��XВведите логи и пароль для входа или регистрации.��6%   $
��Auto��6`   $��Center=�54      $��317,8,317,6q�61   $��Center=�6F   ���	#FFFCFCFC6a   55         CardGameClient.PopupInfo  58   
   -   5          Name$ errorPopupInfo��58       J� Opacity$ 1Q�6/   $y�Hidden=�6;   59      5:      