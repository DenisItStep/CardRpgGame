   M S B A M L   `   `   `  ����I  ECardGameClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CardGameClientCardGameClient  5   	   (    "CardGameClient.InGameMenuEscWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   +dcclr-namespace:CardGameClient     5   	   -   5        $��InGameMenuEscWnd��5   +     � Title$  Avalon: Battles for Glory��6	    � 
ResizeMode$ NoResize=�6C    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   	   $	��768��6-   $
��1024��6=   $	��768��6M   $
��1024��6Z    � Cursor$1 +/CardGameClient;component/Images/cursor.cur��5   	    � WindowStyle$
 None=�6>    � WindowState$ 	Maximized=�6Q    � AllowsTransparency . ���	#C6000000.��5	      � $	��311��6   $	��264��6   $	��311��6$   $	��264��64   ��5
   
   �� 5       �� ImageSource$5 //CardGameClient;component/Images/newCardWnd.png��6   	 �� BitmapScalingMode$
	 Fant=�5      .+�5   
   �� -   5        $��CardNameLabel��5   D   $��Меню��5      
 � UseLayoutRounding
 .  J� SnapsToDevicePixels . $��Center=�6S   $��Center=�5      $��0,35,0,0q�6-   $��20(�6?   $,��&Fonts/MyriadPro-Regular.otf#Myriad Pro)�5      $	��Top=�6Y   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 0Q�6#      RenderingBias$ Quality=�62      	Direction$	 330Q�6J      ShadowDepth$ 3Q�6Z   5         CardGameClient.StyledBtn  5   
   -   5          Name$ 	CancelBtn��5         CardGameClient.StyledBtn   MouseUp$ CancelBtn_MouseUp��5   E   $��Center=�5   *      Text$ Отмена��6G   $��Center=�6U   $��34��5      $	��135��6#   $��0, 0 ,0 ,100q�6/   6E     5   
   -   5        $ LeaveBtn��5      $ LeaveBtn_MouseUp��5   C   $��Center=�5   )   $ Покинуть��6F   $��Center=�6V   $��34��5      $	��135��6#   $��
0, 0 ,0 ,0q�6/   6C     5   
   -   5        $ ExitBtn��5      $ ExitBtn_MouseUp��5   9   $��Center=�5   (   $ 
Выход��6E   $��Center=�6R   $��34��6m   $	��135��5      $��0, 100 ,0 ,0q�6#   69   5      5      