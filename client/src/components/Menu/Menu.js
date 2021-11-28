import React,{useContext, useEffect} from 'react'
import Slider from '@mui/material/Slider';
import DifficultyContext from '../../contexts/DifficultyContext';
import { Link } from 'react-router-dom';

const Menu = () => {

    const { difficulty, setDifficulty } = useContext(DifficultyContext);

    const marks = [
        {
            value: 1,
            label: 'Very Easy',
        },
        {
            value: 20,
            label: 'Easy',
        },
        {
            value: 40,
            label: 'Normal',
        },
        {
            value: 60,
            label: 'Hard',
        },
        {
            value: 81,
            label: 'Evil',
        },
    ];

    const sliderValue = (value) => {
        setDifficulty(value);
        return value;
    }

    const valueLabelFormat = (value) => {
        return marks.findIndex((mark) => mark.value === value) + 1;
    }

    return (
        <div className="flex flex-col mt-5 w-80 text-white">
            <div className="text-white text-center mb-4 text-2xl">
                Select Difficulty
            </div>
            <div className="text-white bg-white p-10 rounded-xl">
                <Slider
                    color="secondary"
                    aria-label="Difficulty"
                    defaultValue={20}
                    getAriaValueText={sliderValue}
                    valueLabelDisplay="auto"
                    step={10}
                    marks
                    min={1}
                    max={81}
                    marks={marks}

                />
            </div>

            <Link  className="bg-purple-700 text-white text-center p-2 mt-4 rounded" to="/game">Start Game</Link>
            
        </div>
    )
}

export default Menu
