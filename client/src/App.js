
import React,{useState} from 'react'
import Game from './layouts/Game'
import Main from './layouts/Main'
import DifficultyContext from './contexts/DifficultyContext'


import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
const App = () => {

  const [difficulty, setDifficulty] = useState(20);
  const value = { difficulty, setDifficulty };




  return (
    <DifficultyContext.Provider value={value}>
    <Router>
      <div className="flex justify-center h-screen items-center bg-gray-800">
        <Routes>
          <Route exact path="/" element={<Main />} />
          <Route exact path="/game" element={<Game />} />
        </Routes>
      </div>
    </Router>
    
    </DifficultyContext.Provider>
  )
}

export default App