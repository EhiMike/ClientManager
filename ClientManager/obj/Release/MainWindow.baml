   M S B A M L   `   `   `  ���� H  DClientManager, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null-clr-namespace:ClientManagerClientManager      ClientManager.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:ClientManager     -     � Title$  Gestione Palestra��$��629.5��$��937.375�� � Icon$) #pack://siteoforigin:,,,/bin/gym.ico��.���  � ColumnDefinitions �� $	��51* ��� $
��163* �.+��� -    ��Name$ tabControlGeneral��$��2��.@��� -    ��Name$ tabItemClient��$��
Anagrafica��.��� ���	#FFE5E5E5 �� $	��13* ��� $	��10* ��� $	��74* ��� $	��20* ��� $	��33* ��� $��0* ��� $
��110* ��� $
��146* ��� $
��422* ��� $	��22* �.+��� -    ��Name$ 	lblClient��$��Cliente��$
��Left=�$��30��$��	10,10,0,0q�$	��Top=�$��49��$��3���� -    ��Name$ tbClient�� �� CharacterCasing$ Upper=�$
��Left=�$��30��$��	41,10,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��138��$��5��$��2��$��Center=��� -   	 ��Name$	 cbClient��$
��Left=�$��30��$��	70,10,0,0q�$	��Top=�$	��294��$��5��$��4��$��Center=��� -   $ lblNome��$
��Nome��$
��Left=�$��30��$��5,46,0,0q�$	��Top=�$��95��$��4���� -   $ 
lblCognome��$��Cognome��$
��Left=�$��30��$��0,46,0,0q�$	��Top=�$	��135��$��8���� -	   $ lblSesso��$��Sesso��$
��Left=�$��30��$��5,81,0,0q�$	��Top=�$	��145��$��5���� -
   $ lblCF��$��Codice Fiscale��$��30��$��
0,81,274,0q�$	��Top=�$��8���� -   $ lblDataNascita��$��Data di Nascita��$
��Left=�$��30��$��	5,116,0,0q�$	��Top=�$	��145��$��5���� -   $ lblLuogoNascita��$��Luogo di nascita��$
��Left=�$��30��$��	0,116,0,0q�$	��Top=�$	��135��$��8���� -   $ lblIndirizzo��$��	Indirizzo��$
��Left=�$��30��$��	5,151,0,0q�$	��Top=�$	��145��$��5���� -   $ lblStato��$��Stato��$
��Left=�$��30��$��327,151,0,0q�$	��Top=�$	��107��$��8��$��2���� -   $ lblProvincia��$��	Provincia��$
��Left=�$��30��$��	0,151,0,0q�$	��Top=�$	��135��$��8���� -   $ lblEmail��$��Email��$
��Left=�$��30��$��	5,186,0,0q�$	��Top=�$	��145��$��5���� -   $ lblTelefono��$��Telefono��$
��Left=�$��30��$��	0,186,0,0q�$	��Top=�$	��135��$��8���� -   $ 
lblScadAbb��$��Scadenza Abbonamento��$
��Left=�$��30��$��	5,221,0,0q�$	��Top=�$	��145��$��5���� -   $ 	lblVisita��$��Scadenza visita medica��$
��Left=�$��30��$��	0,221,0,0q�$	��Top=�$	��135��$��8���� -   $ tabControlClient��$��10��$��
7,293,0,32q�.@��� $��Presenze��.��� ���	#FFE5E5E5 �� $
��108* ��� $	��77* ��� $	��78* ��� $
��235* �.+�&   System.Windows.Controls.DataGrid  -   
  Name$
 dataGrid��$��10,45,10,10q�   CanUserReorderColumns .  $��4��   CanUserSortColumns .  ���� *
Presenza+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Data��   
IsReadOnly .    CanUserSort .    Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$
 Data� �� StringFormat$ {0:dd/MM/yyyy}��  $ Ora In�� . $ *  �� $ OraIngresso�  $ Ora Out�� . $ *  �� $ 	OraUscita��� -    ��Name$ btnIngresso��$��Ingresso��$
��Left=�$��30��$��	10,10,0,0q�$	��Top=�$	��118���� -   $ 	btnUscita��$��Uscita��$
��Left=�$��30��$��
133,10,0,0q�$	��Top=�$	��118��$��2���� -   $ 
lblNomeVal��$��(vuoto)��$
��Left=�$��30��$��	10,46,0,0q�$	��Top=�$	��236��$��3��$��5���� -   $ lblSessoVal��$��(vuoto)��$
��Left=�$��30��$��	10,81,0,0q�$	��Top=�$	��236��$��3��$��5���� -   $ lblDataNascitaVal��$��(vuoto)��$
��Left=�$��30��$��
10,116,0,0q�$	��Top=�$	��236��$��3��$��5���� -   $ lblIndirizzoVal��$��(vuoto)��$
��Left=�$��30��$��
10,151,0,0q�$	��Top=�$	��236��$��3��$��5���� -   $ lblEmailVal��$��(vuoto)��$��28��$��
10,186,0,0q�$	��Top=�$��5��$��3��$
��Left=��� -   $ lblScadAbbVal��$��(vuoto)��$
��Left=�$��30��$��
10,221,0,0q�$	��Top=�$	��236��$��3��$��5���� -   $ lblCognomeVal��$��(vuoto)��$
��Left=�$��30��$��
140,46,0,0q�$	��Top=�$	��259��$��8���� -   $ lblCFVal��$��(vuoto)��$
��Left=�$��30��$��
140,81,0,0q�$	��Top=�$	��259��$��8���� -    $ lblLuogoNascitaVal��$��(vuoto)��$
��Left=�$��30��$��140,116,0,0q�$	��Top=�$	��259��$��8���� -!   $ lblProvinciaVal��$��(vuoto)��$
��Left=�$��30��$��140,151,0,0q�$	��Top=�$	��182��$��8���� -"   $ lblVisitaVal��$��(vuoto)��$
��Left=�$��30��$��140,221,0,0q�$	��Top=�$	��272��$��8���� -#   $ lblTelefonoVal��$��(vuoto)��$
��Left=�$��30��$��140,186,0,0q�$	��Top=�$	��272��$��8���� -$   $ btnNuovo��$��Nuovo��$��8��$
��Left=�$��30��$��
250,10,0,0q�$	��Top=�$��58�� � ToolTip$ Creare nuovo cliente���� -%   $ btnModifica��$��Modifica��$��8��$
��Left=�$��30��$��
313,10,0,0q�$	��Top=�$��58��$ Modifica dati cliente���� -&   $ 
btnElimina��$��Elimina��$��8��$
��Left=�$��30��$��
376,10,0,0q�$	��Top=�$��58��$ Elimina cliente��$��2��{�.  �� -'   $ lblDocumenti��$��Scadenza Abbonamento��$
��Left=�$��30��$��	5,256,0,0q�$	��Top=�$	��145��$��5���� -(   $ 	btnVisita��$��Visita Medica��$��6��$��10,259,10,0q�$	��Top=�$��90���� -)   $ btnCI��$
��C.I.��$��7��$
��Left=�$��20��$��
10,259,0,0q�$	��Top=�$��90�� J� RenderTransformOrigin$ 
2.593,0.45&��� $��	Riepilogo��.��� ���	#FFE5E5E5$��	0,0,0,-34q� �� $��7* ��� $��7* ��� $	��46* ��� $	��28* ��� $
��180* ��� $
��582* �.+�  -*   $
 dataGridRiepilogo��$
��Left=�$	��Top=�$��6�� .  ���� *Utente+   $ *  .  . $
 Nome��   SortDirection$ 	AscendingX� �� $
 Nome�  $ *  .  . $ Cognome��$ 	AscendingX� �� $ Cognome�  $ *  .  . $ Scadenza��$ 	AscendingX� �� $ ScadenzaAbb�$ {0:dd/MM/yyyy}���� $��Contabilità��$	��Top=�.��� ���	#FFE5E5E5 �� $��2* ��� $	��22* ��� $	��19* ��� $
��107* ��� $
��150* ��� $
��623* �.+�  -+   $
 dataGridVariazioni�� .  $��	0,41,0,-7q�$��6������ *VariazioneEconomica+   $ *  . $
 Data�� �� *Data+  $ *  . $ Descrizione Dare�� �� *DescrizioneDare+  $ *  . $ Importo Dare�� �� *ImportoDare+  $ *  . $ Descrizione Avere�� �� *DescrizioneAvere+  $ *  . $ Importo Avere�� �� *ImportoAvere+   RowStyle ��  �� 
TargetType)  #System.Windows.Controls.DataGridRow"   �� Triggers ��  �� Binding �� *DescrizioneDare+ �� Value TOTALE DARE.a���  �� Property � �� �� Value
 � �����  � ��$
 Bold"��� -,   $ txtVariazione��$��
45,4,329,0q�$
 Wrap=�$��4��$��2��$	��Top=�$��25��(  "System.Windows.Controls.DatePicker  --      Name$  datePickerVariazione��$
��Left=�$��25��$��8,4,0,0q�$	��Top=�$	��180��$��4��$��1���� -.   $ txtImportoVariazione��$��25��$��	0,4,123,0q�$
 Wrap=�$��70��$��25��$	��Top=�$��5��$��Stretch=�$��Right=�$��75���� -/   $ btnInserisciVariazione��$��	Inserisci��$��0,4,0,0q�{�.  $��Center=�$��Center=�$	��Top=�$��25��$��5��$	��120��$��Right=�$	��118��	� -0   ! 	�Name$! RbDare��$
��DARE��$��19��$��
0,10,270,0q�$	��Top=�" d� 	IsChecked$
" TrueY�$��5��$��Stretch=�$��54��$��Right=�	� -1   $! rbAvere��$��AVERE��$��19��$��	0,9,198,0q�$	��Top=�$��5��$��Stretch=�$��54��$��Right=�