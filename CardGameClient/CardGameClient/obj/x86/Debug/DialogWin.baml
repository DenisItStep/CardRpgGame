   M S B A M L   `   `   `  ����I  ECardGameClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CardGameClientCardGameClient  5   	       CardGameClient.DialogWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   +dcclr-namespace:CardGameClient     5   	   -   5        $��	DialogWnd��5   +     � Title$  Avalon: Battles for Glory��6	    � 
ResizeMode$ NoResize=�6<    � UseLayoutRounding .  J� SnapsToDevicePixels .  � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   :   $	��768��6^   $
��1024��5   	   $	��768��6   $
��1024��6&    � Cursor$1 +/CardGameClient;component/Images/cursor.cur��63    � WindowStyle$
 None=�5   	    � WindowState$ 	Maximized=�6   	 � AllowsTransparency	 . ���	#AA000000.��5
      � .+�5   
   � $	��230��6   $	��600��6   $��0, -35, 0, 35q�6(   .+�5      �� -   5        $��TextInfo��5      $��14(�6(   $��Verdana)�66   $��0, 0, 0, 35q�6K   $��Center=�5      $N��HУстанавливается соединение. Ожидайте...��65   
 �� TextAlignment$
 Center=�5      $��Center=�6/   
�������$
��Wrap=�6]      CardGameClient.StyledBtn  5      -   5          Name$ 	AcceptBtn��5         CardGameClient.StyledBtn   MouseUp$ AcceptBtnOnly_MouseUp��5      $
��Left=�5   .      Text$
 Да��6I   $��Bottom=�6S   $��34��5      $	��135��6'   $��
157,0,0,35q�63   $y�Hidden=�6G   5        5      -   5        $ AcceptBtnOnly��5      $ AcceptBtnOnly_MouseUp��5   Y   $��Center=�5   2   $
 ОК��6O   $��Bottom=�6Y   $��34��5      $	��135��6'   $��0,0,0,35q�63   $y�Hidden=�6E   6Y     5      -   5        $ 	CancelBtn��5      $ Image_MouseUp��5   [   $��Right=�5   .   $ Отмена��6J   $��Bottom=�6X   $��34��5      $	��135��6'   $��
0,0,157,35q�63   $y�Hidden=�6G   6[   ��5      �� 5       �� ImageSource$3 -/CardGameClient;component/Images/popup_bg.png��6   5      5      5      