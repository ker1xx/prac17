   M S B A M L   `   `   `  ����
  prac17    prac17.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5	      �� 5
      .+�5   
   �� $��
Viseliцa)��6   $��0��6%   $��Center=�62   $��Center=�6M   $��25(�6j   �� 5   
   -   5         ��Name$ CreateGameButton��5      $��2��6+   $&�� Начать новую игру��69   $��Center=�6U   $��Center=�6p   $��20(�6�   $��10q�6�   ��6�   �� *CreateServerCommand+�� 5   
   -   5        $ JoinGameButton��5      $��3��6)   $.��(Присоединиться к игре��67   $��Center=�6W   $��Center=�6r   $��20(�6�   $��10q�6�   ��6�   �� *JoinServerCommand+�� 5   
   -   5         ��Name$ 
EnterLogin��5      $��1��6&   $��Center=�63   $��Center=�6N   $	��300��6k   ��6z   �� *Login+.��6�   ��5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   -   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6-    �� 
TargetType" ��6    �� 	Resources 5         CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch	 !� Visual��%�   &	        !� 5      $
 Left=�6=   $ Center=�6O   $
 None=�6c   	 5      �� 5   "   $��Login��6(   
�������68   5      5      
 �� Triggers
 5      P� 5       P� Property � �� �� Empty P� Value# Z 62   . �5      ��  �� Property � �� �� Value# [  6;   5      P� 5       � �� 62   Z� . �5      ��  � ��# [  6;   5      P� 5        �@J�IsKeyboardFocused . . �5!      ��  � ��
 �����6;   5"      5$      .��5%      5&      5'      5(      