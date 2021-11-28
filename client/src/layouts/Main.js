import React from 'react'
import Menu from '../components/Menu/Menu'
import SudokuLogo from '../assets/sudoku-logo.svg'
const Main = () => {
    return (
        <div className="flex flex-col items-center">
            <img src={SudokuLogo} className="w-56" alt="Sudoku Logo" />
            <Menu />
        </div>
    )
}

export default Main
