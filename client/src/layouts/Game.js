import React from 'react'
import Board from '../components/Board/Board'
import DifficultyContext from '../contexts/DifficultyContext'

const Game = () => {
    const { difficulty, setDifficulty } = React.useContext(DifficultyContext)

    return (
        <div>
            <div>
                <Board difficulty={difficulty} />
            </div>
           
        </div>
    )
}

export default Game
