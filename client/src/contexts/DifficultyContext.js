import React,{createContext} from "react";

const DifficultyContext = createContext({
    difficulty: {},
    setDifficulty: () => {},
  });

export default DifficultyContext;