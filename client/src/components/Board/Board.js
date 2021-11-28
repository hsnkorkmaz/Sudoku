import React, { useEffect } from 'react'
import CellInput from './CellInput'
import axios from 'axios'
import { Link } from 'react-router-dom'

const Board = ({ difficulty }) => {

    const [board, setBoard] = React.useState()
    const [newNumbers, setNewNumbers] = React.useState([])
    const [rowHover, setRowHover] = React.useState(null);
    const [colHover, setColHover] = React.useState(null);
    const [message, setMessage] = React.useState('');

    const [isCompleted, setIsCompleted] = React.useState(false);


    useEffect(() => {
        axios.get('https://hasansudokuapi.azurewebsites.net/api/Sudoku/GenerateBoard?difficulty=' + difficulty)
            .then(res => {
                setBoard(res.data)
                setNewNumbers([...res.data.numbers]);
            })
    }, [])



    




    return (
        <div>
            <div className={message === "" ? "hidden": "text-center bg-purple-700 text-white rounded mb-5 p-5 text-xl"}>{message}</div>
            <div className="grid grid-cols-9 border-2 border-black">
                {board?.numbers.map((item, index) => {
                    return (
                        <CellInput
                            key={index}
                            item={item}
                            index={index}
                            setRowHover={setRowHover}
                            rowHover={rowHover}
                            setColHover={setColHover}
                            colHover={colHover}
                            newNumbers={newNumbers}
                            setNewNumbers={setNewNumbers}
                            setIsCompleted={setIsCompleted} />
                    )
                }
                )}

            </div>

            <div className="flex justify-between mt-10">
                <Link className="bg-purple-700 text-white text-center p-2 mt-4 rounded" to="/">Go Back</Link>


                <button className="bg-purple-700 text-white text-center p-2 mt-4 rounded" onClick={() => {
                    axios.post('https://hasansudokuapi.azurewebsites.net/api/Sudoku/SolveBoard', {
                        numbers: board.numbers,
                    })
                        .then(res => {
                            setBoard(res.data);
                            setNewNumbers(res.data.numbers);
                            setMessage('Congratulations you are amazing :)');
                        })
                        .catch(err => {
                            console.log(err);
                            setMessage('Something went wrong')
                        })
                }}>Solve</button>


                <button className="bg-purple-700 text-white text-center p-2 mt-4 rounded" onClick={() => {
                  
                       if (newNumbers.filter(item => item === 0).length > 0) {

                        
                        setMessage('There are empty cells')
                    } else {
                        axios.post('https://hasansudokuapi.azurewebsites.net/api/Sudoku/ValidateBoard', {
                            numbers: newNumbers
                        })
                            .then(res => {
                                if (res.data.isValid) {
                                    setMessage('You have solved, this one was easy try harder :)')
                                } else {
                                    setMessage('The board is not valid, check again!')
                                }
                            })
                    }
                }}>Check My Solution</button>





            </div>
        </div>
    )
}

export default Board
