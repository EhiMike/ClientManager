   M S B A M L   `   `   `  ���� H  DClientManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClientManagerClientManager      ClientManager.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:ClientManager     -     � Title$  
MainWindow��$��629.5��$��864.375��.��� .+��� -    ��Name$ tabControlGeneral��.@��� -    ��Name$ tabItemClient��$��Clienti��.��� ���	#FFE5E5E5 � ColumnDefinitions �� $	��13* ��� $
��104* ��� $	��33* ��� $��0* ��� $
��110* ��� $
��146* ��� $
��422* ��� $	��22* �.+��� -    ��Name$ 	lblClient��$��Cliente��$
��Left=�$��30��$��	10,10,0,0q�$	��Top=�$��49��$��2���� -    ��Name$ tbClient�� �� CharacterCasing$ Upper=�$
��Left=�$��30��$��	51,10,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��138��$��4��$��1��$��Center=��� -    ��Name$ cbClient��$
��Left=�$��30��$��	70,10,0,0q�$	��Top=�$	��294��$��3��$��4��$��Center=��� -   $ lblNome��$
��Nome��$
��Left=�$��30��$��5,46,0,0q�$	��Top=�$��95��$��2���� -   $ 
lblCognome��$��Cognome��$
��Left=�$��30��$��0,46,0,0q�$	��Top=�$	��135��$��6���� -	   $ lblSesso��$��Sesso��$
��Left=�$��30��$��5,81,0,0q�$	��Top=�$	��145��$��3���� -
   $ lblCF��$��Codice Fiscale��$��30��$��
0,81,274,0q�$	��Top=�$��6���� -   $ lblDataNascita��$��Data di Nascita��$
��Left=�$��30��$��	5,116,0,0q�$	��Top=�$	��145��$��3���� -   $ lblLuogoNascita��$��Luogo di nascita��$
��Left=�$��30��$��	0,116,0,0q�$	��Top=�$	��135��$��6���� -   $ lblIndirizzo��$��	Indirizzo��$
��Left=�$��30��$��	5,151,0,0q�$	��Top=�$	��145��$��3���� -   $ lblStato��$��Stato��$
��Left=�$��30��$��327,151,0,0q�$	��Top=�$	��107��$��6��$��2���� -   $ lblProvincia��$��	Provincia��$
��Left=�$��30��$��	0,151,0,0q�$	��Top=�$	��135��$��6���� -   $ lblEmail��$��Email��$
��Left=�$��30��$��	5,186,0,0q�$	��Top=�$	��145��$��3���� -   $ lblTelefono��$��Telefono��$
��Left=�$��30��$��	0,186,0,0q�$	��Top=�$	��135��$��6���� -   $ 
lblScadAbb��$��Scadenza Abbonamento��$
��Left=�$��30��$��	5,221,0,0q�$	��Top=�$	��145��$��3���� -   $ 	lblVisita��$��Scadenza visita medica��$
��Left=�$��30��$��	0,221,0,0q�$	��Top=�$	��135��$��6���� -   $ tabControlClient��$��8��$��
7,257,0,32q�.@��� $��Presenze��.��� ���	#FFE5E5E5 �� $
��108* ��� $	��77* ��� $	��78* ��� $
��235* �.+��� -   $ lblClientAbb��$��Abbonamento:��$
��Left=�$��30��$��0,10,0,0q�$	��Top=�$��94��&   System.Windows.Controls.DataGrid  -   	  Name$	 dataGrid��$��10,45,10,10q�
   CanUserReorderColumns
 .  $��4��   CanUserSortColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Data��   
IsReadOnly .    CanUserSort .    Width5  /System.Windows.Controls.DataGridLengthConverter$ *   $ Ora In�� . $ *   $ Ora Out�� . $ * �� -   $ lblClientAbbVal��$��Abbonamento:��$
��Left=�$��30��$��
118,10,0,0q�$	��Top=�$	��162��$��2���� $��	Documenti��$��22��$��Bottom=�.��� ���	#FFE5E5E5.+��� -    ��Name$ 
btnApriDoc��$
��Apri��$
��Left=�$��28��$��18,6,0,0q�$	��Top=�$��82�� � ToolTip$ Apri documento���� -   $ 	btnAddDoc��$��Aggiungi��$
��Left=�$��28��$��	105,6,0,0q�$	��Top=�$��82��$ Aggiungi documento��(  "System.Windows.Controls.WebBrowser  -     Name$ 
webBrowser��$
��Left=�$	��203��$��	10,39,0,0q�$	��Top=�$	��817���� -   $ 
lblNomeVal��$
��Nome��$
��Left=�$��30��$��	10,46,0,0q�$	��Top=�$	��236��$��3��$��3���� -   $ lblSessoVal��$
��Nome��$
��Left=�$��30��$��	10,81,0,0q�$	��Top=�$	��236��$��3��$��3���� -   $ lblDataNascitaVal��$
��Nome��$
��Left=�$��30��$��
10,116,0,0q�$	��Top=�$	��236��$��3��$��3���� -   $ lblIndirizzoVal��$
��Nome��$
��Left=�$��30��$��
10,151,0,0q�$	��Top=�$	��236��$��3��$��3���� -   $ lblEmailVal��$
��Nome��$
��Left=�$��30��$��
10,186,0,0q�$	��Top=�$��3��$��3���� -    $ lblScadAbbVal��$
��Nome��$
��Left=�$��30��$��
10,221,0,0q�$	��Top=�$	��236��$��3��$��3���� -!   $ lblCognomeVal��$��Cognome��$
��Left=�$��30��$��
140,46,0,0q�$	��Top=�$	��259��$��6���� -"   $ lblCFVal��$��Cognome��$
��Left=�$��30��$��
140,81,0,0q�$	��Top=�$	��259��$��6���� -#   $ lblLuogoNascitaVal��$��Cognome��$
��Left=�$��30��$��140,116,0,0q�$	��Top=�$	��259��$��6���� -$   $ lblProvinciaVal��$��Cognome��$
��Left=�$��30��$��140,151,0,0q�$	��Top=�$	��182��$��6���� -%   $ lblVisitaVal��$��Cognome��$
��Left=�$��30��$��140,221,0,0q�$	��Top=�$	��272��$��6���� -&   $ lblTelefonoVal��$��Cognome��$
��Left=�$��30��$��140,186,0,0q�$	��Top=�$	��272��$��6���� -'   $ btnNuovo��$��Nuovo��$��6��$
��Left=�$��30��$��
250,10,0,0q�$	��Top=�$��58��$ Creare nuovo cliente���� -(   $ btnModifica��$��Modifica��$��6��$
��Left=�$��30��$��
313,10,0,0q�$	��Top=�$��58��$ Modifica dati cliente���� -)   $ 
btnElimina��$��Elimina��$��6��$
��Left=�$��30��$��
376,10,0,0q�$	��Top=�$��58��$ Elimina cliente��$��2���� $��Spese Palestra��.��� ���	#FFE5E5E5$��	0,0,0,-34q�