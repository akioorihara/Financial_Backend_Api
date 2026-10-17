import { useState } from "react";

function Square() {
  const [count, setCount] = useState(null);

  function handleClick() {
    setCount(count + 1);
  }

  return (
    <button className="square" onClick={handleClick}>
      {count}
    </button>
  );
}

export default function Board() {
  return (
    <>
      <div className="board-row">
        <Square />
        <Square />
        <Square />
      </div>
      <div className="board-row">
        <Square />
        <Square />
        <Square />
      </div>
      <div className="board-row">
        <Square />
        <Square />
        <Square />
      </div>
    </>
  );
}
