   M S B A M L   `   `   `  ����N  JRockPaperScissorsAI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:RockPaperScissorsAIRockPaperScissorsAI  5   	   $    RockPaperScissorsAI.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:RockPaperScissorsAI     5   	     � Title$  
MainWindow��5   	   $	��800��6   $	��800��6)    � 	Resources 5
         NoHoverButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType��%B  &	        �� 5   
   " ��6+   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 ���� 6,   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��162* �6   �� 5      $
��227* �6    � ColumnDefinitions 5    
   �� 5!      $��3* �6   �� 5"      $��3* �6   �� 5#      $��3* �6   .+�5%   
   �� -   5         ��Name$
 Rock��5%      $�� ��6   $
��Left=�6*   $��	48,65,0,0q�6E   $	��170��6X   $	��170��6d   $	��Top=�6q   $��1��6�   	 � Cursor$
	 Hand��6�   
 �� IsCancel
 . #��[  6�   ��5&      �� 5'       �� ImageSource$ 	/Rock.png��6   5)      �� 5*   
   -   5        $ Scissors��5*      $�� ��6#   $
��Left=�6.   $��	48,65,0,0q�6I   $	��170��6\   $	��170��6h   $	��Top=�6u   $��1��6�   $
	 Hand��6�   
 . $��1��6�   #��[  6�   ��5+      �� 5,      $ /Scissors.png��6   5.      �� 5/   
   -   5        $ Paper��5/      $�� ��6    $
��Left=�6+   $��	49,65,0,0q�6F   $	��170��6Y   $	��170��6e   $	��Top=�6r   $��1��6�   $
	 Hand��6�   
 . $��2��6�   #��[  6�   ��50      �� 51      $ 
/Paper.png��6   53      �� 54   
   -   5        $ Mystery��54      $�� ��6"   $
��Left=�6-   $	��170��6H   $	��170��6T   $��Center=�6a   $	 Cross��6|   
 . $��1��6�   #��[  6�   $��48,0,0,0q�6�   ��55      �� 56      $ /Mystery.png��6   58      5:      5;      