   M S B A M L   `   `   `  ����A  =Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TetrisTetris  5   	       Tetris.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Tetris     5   	   -   5          � Title$  Tetris��5   	   $	��350��6   $	��525��6%   .��5
      � -   5        $��MY_BOARD��5
       � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   A� -   5        $��MY_GRID��5      $��0��6%   $��5��65   5      A� 5   
   -   5        $��PREVIEW_GRID��5      $��1��6*   $��0��6:   $��10,10,10,10q�6G   5      �� 5    
   $��1��6   $��1��6"   .+�5!      �� $��Score��6   $��Center=�6&   $��20(�6C   $
��Bold"�6Q   �� 5"      -   5        $��SCORE��5"      $��Center=�6&   $��15(�6C   $
��Bold"�6Q   5#      �� 5%   
   $��2��6   $��1��6"   .+�5&      �� $��Level��6   $��Center=�6&   $��15(�6C   $
��Bold"�6Q   �� 5'      -   5        $��LEVEL��5'      $��Center=�6&   $��15(�6C   $
��Bold"�6Q   5(      �� 5*   
   $��3��6   $��1��6"   .+�5+      �� $��Rows Destroyed��6   $��Center=�6/   $��15(�6L   $
��Bold"�6Z   �� 5,      -   5        $��ROWS_DESTROYED��5,      $��Center=�6/   $��15(�6L   $
��Bold"�6Z   5-      �� 5/   
   $��4��6   $��1��6"   .+�50      �� $��Elapsed Time��6   $��Center=�6-   $��15(�6J   $
��Bold"�6X   �� 51      -   5        $
��TIME��51      $��Center=�6%   $��15(�6B   $
��Bold"�6P   52      �� 54   
   -	   5        $��MESSAGE��55      $��Press space to start��54       �� TextAlignment$ Center=�56      $��Center=�57      $��Center=�58      $��0��6/   $��1��59      $��2��5:      
���   �$��25(�5<      $
��Bold"�5=      $y�Visible=�5>      
��� ���5?      5@      5B      