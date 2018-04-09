using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TableroDeAjedrez.Modelos;
using TableroDeAjedrez.Controles;

namespace TableroDeAjedrez
{
    public partial class frmAjedrez : Form
    {
        public frmAjedrez()
        {
            InitializeComponent();
        }
        
        private Game _game = new Game();
        private List<SquareControl> _squares = new List<SquareControl>();

        /// <summary>
        /// Dibuja el tablero en el panel de 8x8 en verde y gris.
        /// </summary>
        private void FormarTablero()
        {
            // Creamos los controles para las celdas del tablero
            foreach (Square square in _game.Board)
            {
                SquareControl control = new SquareControl();
                control.BoardSquare = square;
                _squares.Add(control);
                control.CheckedChanged += Square_CheckedChanged;
            }
            pnTABLERO.Controls.AddRange(_squares.ToArray());
            // Controlador para el evento de cambio de estado del juego
            _game.StateChanged += Game_StateChanged;
            // Inicia el juego colocando en la parte superior el color seleccionado
            _game.Start(rbFICHASNEGRAS.Checked ? PlayerColor.Black : PlayerColor.White);
            rbFICHASBLANCAS.Checked = true;
        
        }

        /// <summary>
        /// Método que controla la pulsación de una celda por parte del usuario
        /// </summary>
        private void Square_CheckedChanged(object sender, EventArgs e)
        {
            SquareControl square = (SquareControl)sender;
            if (square.Checked)
            {
                if (_game.State == GameState.PendingForWhiteMove || _game.State == GameState.PendingForBlackMove)
                {
                    // Si el juego está pendiente de iniciar movimiento se selecciona la celda pulsada
                    _game.SelectPiece(square.BoardSquare);
                }
                else if (_game.State == GameState.WhitePlayerMoving || _game.State == GameState.BlackPlayerMoving)
                {
                    // Si el juego está pendiente de finalizar movimiento se ejecuta el movimiento correspondiente
                    // y se deseleccionan las celdas del movimiento
                    if (_game.MoveToSquare(square.BoardSquare))
                    {
                        square.Checked = false;
                       // _squares.First(s => s.Checked).Checked = false;

                    }
                }
            }
            else
            {
                // Si el juego está pendiente de completar el movimiento se deselecciona la celda
                if (_game.State == GameState.WhitePlayerMoving || _game.State == GameState.BlackPlayerMoving)
                    _game.UnselectPiece();
            }
        }

        /// <summary>
        /// Controlador para el evento de cambio de estado del juego
        /// </summary>
        private void Game_StateChanged(object sender, EventArgs e)
        {
            switch (_game.State)
            {
                case GameState.PendingForWhiteMove:
                case GameState.PendingForBlackMove:
                    // Si el juego está pendiente de inciar movimiento habilitamos las celdas
                    // seleccionables (las que tienen piezas del color del jugador)
                    IEnumerable<Square> selectionableSquares = _game.GetSquaresThatCanBeSelected();
                    foreach (SquareControl square in _squares)
                    {
                       // square.Enabled = selectionableSquares.Contains(square.BoardSquare);
                        square.FlatAppearance.BorderSize = 1;
                    }
                    break;
                case GameState.WhitePlayerMoving:
                case GameState.BlackPlayerMoving:
                    // Si el juego está pendiente de finalizar el movimiento habilitamos:
                    // - las celdas posible destino del movimiento
                    // - la celda inicio del movimiento para dar la posibilidad de deseleccionarla
                    // También establecemos un borde en las celdas destino para marcárselas al usuario
                    IEnumerable<Square> destinationSquares = _game.PosibleDestinationSquares();
                    foreach (SquareControl square in _squares)
                    {
                        //square.Enabled = destinationSquares.Contains(square.BoardSquare)
                           // || square.BoardSquare == _game.SelectedSquare;
                        if (square.Enabled)
                            square.FlatAppearance.BorderSize = 1;
                    }
                    break;
                case GameState.WhitePlayerWin:
                case GameState.BlackPlayerWin:
                    // Si ha finalizado el juego deshabilitamos todas las celdas
                    foreach (SquareControl square in _squares)
                    {
                        square.Enabled = false;
                        square.FlatAppearance.BorderSize = 1;
                    }
                    break;
            }
        }

        /// <summary>
        /// Al redimensionar el formulario se redimensiona el panel del tablero
        /// </summary>
        private void ChessGameForm_Resize(object sender, EventArgs e)
        {
            pnTABLERO.Width = Width - 180;
        }

        private void frmAjedrez_Load(object sender, EventArgs e)
        {
            FormarTablero();
        }

        private void toolStripButton2_Click(object sender, EventArgs e) => this.Close();

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _game.Start(rbFICHASNEGRAS.Checked ? PlayerColor.Black : PlayerColor.White);
        }
    }
}
