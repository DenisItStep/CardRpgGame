   M S B A M L   `   `   `  ����I  ECardGameClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CardGameClientCardGameClient  5      "    CardGameClient.inGameTopInfo   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +dcclr-namespace:CardGameClient     5      $	��110��5   7   $	��750��6D   ��5	      �� 5
   
     �� ImageSource$/  )/CardGameClient;component/Images/menu.png��6   .��5      � .+�5   
      CardGameClient.Digit  -   5          Name$ 
user1Level��5      $��81,43,639,43q�6'     5   
   -   5        $ 
user2Level��5      $��639,43,81,43q�6'      CardGameClient.StyledBtn  5   
   -   5          Name$ turnBtn��5         Text$  ��6(      TextFontSize$ 15Q�60   $��Center=�6B   $��Bottom=�5      $	��111��62   $	��110��6>   ��5      �� 5      $2  ,/CardGameClient;component/Images/btnTurn.png��6      BackgroundNormal 5      �� 5      $2  ,/CardGameClient;component/Images/btnTurn.png��6      BackgroundHover 5      �� 5      $2  ,/CardGameClient;component/Images/btnTurn.png��6   5      �� 5   
   -   5        $��firstUserNick��5      $��user1��6%   $
��Auto��65   $
��Left=�6C   $	��Top=�6^   $��Center=�5      $1��+/CardGameClient;component/Fonts/#Myriad Pro)�64   
�������$��13(�5   #    J� RenderTransformOrigin$ 0.5,0.5&�61   $��
134,40,0,0q�6Q   $��82��6e    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      	   
BlurRadius$	 0Q�6#   
   RenderingBias$
 Quality=�62      	Direction$	 330Q�6J      ShadowDepth$ 3Q�6Z   z�5!      V� 5"      .~�5#      ��  �� ScaleX$	 1.2Q�6%   �� 5$      �� 5%      T� 5&      5'      5)      �� 5*   
   -   5        $��twoUserNick��5*      $��user2��6#   $1��+/CardGameClient;component/Fonts/#Myriad Pro)�63   $��13(�5+      
�������$
��Auto��61   $��Right=�6?   $��Center=�5,      $ 0.5,0.5&�64   $	��Top=�6T   $��82��6l   $��
0,40,134,0q�5-       5.        5/      $	 0Q�6#   $	 330Q�62   $
 Quality=�6B   $ 3Q�6Z   z�51      V� 52      .~�53      �� $	 1.2Q�6%   �� 54      �� 55      T� 56      57      59      �� 5:   
   -   5        $��	turnLabel��5:      $
��Auto��6%   $��Center=�63   $��Center=�6P   $��Verdana)�5;      
�������$��13(�6<   $
��Wrap=�6J   $��75��6^    �� TextAlignment$ Center=�5<      $
��None=�6+   $��0,-6,0,0q�6a    5=        5>      $	 0Q�6#   $
 Quality=�62   $	 330Q�6J   $ 3Q�6Z   5@      5A      5B      