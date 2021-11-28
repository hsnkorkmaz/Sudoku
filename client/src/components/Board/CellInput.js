import React, { useEffect } from 'react'
import axios from 'axios';

const CellInput = ({ item, index, rowHover, setRowHover, colHover, setColHover, newNumbers, setNewNumbers, setIsCompleted }) => {
    const gridSize = 9;
    const [style, setStyle] = React.useState("w-10 h-10 text-lg md:text-2xl md:w-20 md:h-20 bg-white-200 text-center cursor-pointer shadow ");
    
    const [rowIndex, setRowIndex] = React.useState(Math.floor(index / gridSize));
    const [colIndex, setColIndex] = React.useState(index % gridSize);
    const [isPossible, setIsPossible] = React.useState(true);

    useEffect(() => {

        if (rowIndex % 3 === 0 && rowIndex !== 0) {
            setStyle(style + " border-t-2 border-gray-400 ");
        }
        if (colIndex % 3 === 0 && colIndex !== 0) {
            setStyle(style + " border-l-2 border-gray-400 ");
        }
        if (rowIndex % 3 === 0 && rowIndex !== 0 && colIndex % 3 === 0 && colIndex !== 0) {
            setStyle(style + " border-t-2 border-l-2 border-gray-400 ");
        }

    }, []);



    const handleCellChange = (index, value) => {
        axios.post('https://hasansudokuapi.azurewebsites.net/api/Sudoku/IsPossible', {
            "numbers": newNumbers,
            "index": index,
            "value": value
        })
            .then(function (response) {
                console.log(response.data);
                if (response.data.isPossible) {
                    setIsPossible(true);
                    setIsCompleted(true);
                }
                else {
                    setIsPossible(false);
                    setIsCompleted(true);
                }
                let tempNumbers = newNumbers;
                tempNumbers[index] = parseInt(value);
                setNewNumbers(tempNumbers);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    return (
        <div className={item !== 0 ? "text-pink-700": ""} key={item} >
            <input
                className={!isPossible ? 
                    style + "bg-red-400 "
                    : (rowHover !== null && rowHover === rowIndex) || (colHover !== null && colHover === colIndex) ?
                    style + "bg-blue-50 "
                    : style}
                    
                type="number" defaultValue={item !== 0 ? item : ""} index={index}
                readOnly={item !== 0}
                maxLength="1"
                onMouseEnter={() => {
                    setRowHover(rowIndex);
                    setColHover(colIndex);
                }}
                onMouseLeave={() => {
                    setRowHover(null);
                    setColHover(null);
                }}

                onFocus={(e) => {
                    e.target.select();
                }}


                onChange={(e) => {
                    if (e.target.value === "0") {
                        e.target.value = "";
                        setIsPossible(true);
                    }
                    if (e.target.value.length > 1) {
                        e.target.value = e.target.value.slice(0, 1);
                        setIsPossible(true);
                    }
                    if (e.target.value.length === 0) {
                        setIsPossible(true);
                        e.target.value = "";
                    }
                    if (e.target.value.length === 1) {
                        setIsPossible(true);
                        handleCellChange(index, e.target.value,);
                    }
                }}
            />
        </div>
    )
}

export default CellInput
