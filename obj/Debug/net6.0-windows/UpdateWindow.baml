   M S B A M L   `   `   `  ����G  CCRUD_Workout, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CRUD_WorkoutCRUD_Workout  5   	       CRUD_Workout.UpdateWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:CRUD_Workout     5   	     � Title$  Aktualizuj ćwiczenie��5   	   $	��450��5	   	   $	��800��5
   	   $��16(�5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��9* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��2��6   $��Nazwa ćwiczenia:��6-   $
��Bold"�6H   �� 5   
   $��0��6   $��3��6   $��Czas trwania:��6-   $
��Bold"�6E   �� 5   
   -   5        $��txtName��5   /   $��1��6   $��2��6   ${�False��6>   �� 5   
   -   5        $��txtDuration��5   /   $��1��6   $��3��6   ${�False��6B   �� 5   
   -   5        $��imgPhoto��5   A   $��2��6   $��0��6   $��2��6-   6A   �� 5   
   -   5        $��txtDescription��5   C   $��2��6   $��2��6   $��2��6/    �� TextWrapping$
 Wrap=�6Y   �� 5    
   -   5        $��btnLoadPhoto��5    T      ButtonStyle#���   6   $��3��67   $��0��6D   $��Wczytaj zdjęcie��6h   $��2q�6�   6�   �� 5!   
   -   5        $��btnSave��5!   T   #���   6   $��3��67   $��2��6D   $��Zapisz��6c   $��2q�6t   6   �� 5"   
   -   5        $��btnClose��5"   T   #���   6   $��3��67   $��3��6D   $��Zamknij��6d   $��2q�6v   6�   5#      5$      