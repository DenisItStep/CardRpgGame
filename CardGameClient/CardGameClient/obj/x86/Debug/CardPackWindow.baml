   M S B A M L   `   `   `  ����I  ECardGameClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CardGameClientCardGameClient  5   	   #    CardGameClient.CardPackWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   +dcclr-namespace:CardGameClient     5   	   -   5        $��CardPackWnd��5   +     � Title$  Avalon: Battles for Glory��6	    � 
ResizeMode$ NoResize=�6>    � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=�5   	   $	��768��6.   $
��1024��6>   $	��768��6N   $
��1024��6[    � Cursor$1 +/CardGameClient;component/Images/cursor.cur��5   	    � WindowStyle$
 None=�6>    � WindowState$ 	Maximized=�6Q    � AllowsTransparency . ���	#C6000000.��5	      � $	��311��6   $	��588��6   $	��311��6*   $	��588��67   $	��311��6C   $	��588��6S    � UseLayoutRounding . 	 J� SnapsToDevicePixels	 . ��5   
   �� 5      
 �� ImageSource$:
 4/CardGameClient;component/Images/newCardWnd_wide.png��6    �� BitmapScalingMode$
 Fant=�5      .+�5   
   �� $��Новые карты!��6    . 	 . $��Center=�5      $��Center=�64   $��0,17,0,0q�6Q   $��16(�6c   $,��&Fonts/MyriadPro-Regular.otf#Myriad Pro)�5      $	��Top=�6D   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 0Q�6#      RenderingBias$ Quality=�62      	Direction$	 330Q�6J      ShadowDepth$ 3Q�6Z   5      �� 5   
   -   5        $��CardNameLabel1��5   D   $��Воин��5       . 	 . $��Center=�6S   $��Center=�5      $��
0,39,300,0q�6-   $��16(�6A   $,��&Fonts/MyriadPro-Regular.otf#Myriad Pro)�5      $	��Top=�6Z   
������� 5        5      $ 0Q�6#   $ Quality=�62   $	 330Q�6J   $ 3Q�6Z   5         CardGameClient.CardPlace  5   
   -   5          Name$ NewCardPlace1��5         
IsMineCard .    inGame .  $	��131��6O   $	��169��6[   $��0,65,300,77q�5       $��Center=�6,   $��Center=�6I   {�.  6d   �� 5"   
   -   5        $��CardNameLabel2��5$   D   $��Лучник��5"       . 	 . $��Center=�6U   $��Center=�5#      $��0,39,0,0q�6-   $��16(�6?   $,��&Fonts/MyriadPro-Regular.otf#Myriad Pro)�5$      $	��Top=�6Z   
������� 5&        5'      $ 0Q�6#   $ Quality=�62   $	 330Q�6J   $ 3Q�6Z   5)        5*   
   -   5        $ NewCardPlace2��5*       .  .  $	��131��6O   $	��169��6[   $��	0,65,0,77q�5+      $��Center=�6*   $��Center=�6G   {�.  6b   �� 5.   
   -   5        $��CardNameLabel3��50   D   $��Маг��5.       . 	 . $��Center=�6R   $��Center=�5/      $��
300,39,0,0q�6-   $��16(�6A   $,��&Fonts/MyriadPro-Regular.otf#Myriad Pro)�50      $	��Top=�6Z   
������� 52        53      $ 0Q�6#   $ Quality=�62   $	 330Q�6J   $ 3Q�6Z   55        56   
   -   5        $ NewCardPlace3��56       .  .  $	��131��6O   $	��169��6[   $��300,65,0,77q�57      $��Center=�6,   $��Center=�6I   {�.  6d      CardGameClient.StyledBtn  59   
      CardGameClient.StyledBtn   MouseUp$ Image_MouseUp��5:   )   $��Center=�59         Text$ Принять��64   $��Bottom=�6C   $��34��6^   $	��135��6j   $��0,0,0,32q�5:      6)   5;      5<      