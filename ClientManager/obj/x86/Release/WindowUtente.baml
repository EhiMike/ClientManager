   M S B A M L   `   `   `  ���� H  DClientManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClientManagerClientManager       ClientManager.WindowUtente   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:ClientManager       � Title$  WindowUtente��$��364.125��$��838.5��.��� $��0,0,2,1q�$��Right=�$	��818�� � RowDefinitions �� $��3* ��� $	��10* ��� $	��33* ��� $	��20* ��� $
��266* � � ColumnDefinitions �� $
��167* ��� $
��157* ��� $	��31* ��� $	��46* ��� $
��120* ��� $
��307* �.+��� -    ��Name$ lblNome��$
��Nome��$
��Left=�$��	5,7,0,249q�$	��145��$��1��$��4���� -   $ 
lblCognome��$��Cognome��$
��Left=�$��30��$��5,32,0,0q�$	��Top=�$	��145��$��2��$��3���� -   $ lblSesso��$��Sesso��$
��Left=�$��15,7,0,6q�$	��105��$��4��$��1��$��2��$��2���� -   $ lblCF��$��Codice Fiscale��$��30��$��
5,14,156,0q�$	��Top=�$��2��$��4���� -   $ lblDataNascita��$��Data di Nascita��$
��Left=�$��30��$��	15,32,0,0q�$	��Top=�$	��105��$��4��$��2��$��3���� -   $ lblLuogoNascita��$��Luogo di nascita��$��Right=�$��30��$��
0,14,302,0q�$	��Top=�$	��105��$��4��$��4��$��2���� -   $ lblIndirizzo��$��	Indirizzo��$
��Left=�$��30��$��5,49,0,0q�$	��Top=�$	��145��$��4���� -   $ lblStato��$��Stato��$
��Left=�$��30��$��
124,49,0,0q�$	��Top=�$��47��$��5��$��4���� -	   $ lblProvincia��$��	Provincia��$
��Left=�$��30��$��	15,49,0,0q�$	��Top=�$	��105��$��4��$��4���� -
   $ lblEmail��$��Email��$
��Left=�$��30��$��5,84,0,0q�$	��Top=�$	��145��$��4���� -   $ lblTelefono��$��Telefono��$
��Left=�$��30��$��	5,119,0,0q�$	��Top=�$	��145��$��4���� -   $ 
lblScadAbb��$��Scadenza Abbonamento��$
��Left=�$��30��$��	5,154,0,0q�$	��Top=�$	��145��$��4���� -   $ 	lblVisita��$��Scadenza Visita Medica��$
��Left=�$��30��$��
17,154,0,0q�$	��Top=�$	��145��$��4��$��2��$��4���� -    ��Name$ textBoxNome�� �� CharacterCasing$ Upper=�$
��Left=�$��30��$��	155,7,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��231��$��4��$��1��$��2��$��Center=��� -   $ textBoxCognome��$ Upper=�$
��Left=�$��30��$��
155,32,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��231��$��4��$��2��$��3��$��Center=��� -   $ 	textBoxCF��$ Upper=�$
��Left=�$��30��$��
155,14,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��231��$��4��$��4�� �� 	MaxLength$ 16��$��Center=�	� -    	�Name$ rbSessoM��$��M��$��5��$
��Left=�$��21��$��5,6,0,0q�$	��Top=�$��65��$��2��	 	� 	GroupName$	 radioGroupSesso��	� -   $ rbSessoF��$��F��$��5��$
��Left=�$��21��$��90,6,0,0q�$	��Top=�$��65��$��2��$	 radioGroupSesso��(  "System.Windows.Controls.DatePicker  -   
  Name$
 datePickerNascita��$��Right=�$��30��$��	0,0,153,0q�$	��Top=�$	��150��$��5��$��3��$��2���� -   $ textBoxLuogoNascita��$ Upper=�$
��Left=�$��30��$��0,14,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��294��$��5��$��4��$��Center=��� -   $ textBoxIndirizzo��$ Upper=�$
��Left=�$��30��$��
155,47,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��231��$��4��$��4��$��Center=��� -    ��Name$ comboBoxProvincia��$��5��$
��Left=�$��30��$��1,50,0,0q�$��4��$	��Top=�$	��118���� -   $ comboBoxStato��$��5��$
��Left=�$��30��$��
176,49,0,0q�$��4��$	��Top=�$	��118���� -   $ textBoxMail��$ Lower=�$
��Left=�$��30��$��
155,84,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��653��$��6��$��4��$��Center=��� -   $ textBoxTelefono��$ Upper=�$
��Left=�$��30��$��155,119,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��654��$��6��$��4��$��Center=�  -   $
 datePickerAbb��$��4��$
��Left=�$��30��$��155,154,0,0q�$	��Top=�$	��231��$��4��  -   $
 datePickerVM��$
��Left=�$��30��$��
48,154,0,0q�$	��Top=�$	��245��$��4��$��5���� -    ��Name$ btnConferma��$��3��$��Conferma��$��2��$
��Left=�$��29��$��	0,214,0,0q�$��4��$	��Top=�$��80���� -   $ 
btnAnnulla��$��Annulla��$��4��$
��Left=�$��29��$��
15,214,0,0q�$��4��$	��Top=�$��80��