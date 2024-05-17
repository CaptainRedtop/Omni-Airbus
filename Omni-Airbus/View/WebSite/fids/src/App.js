import React, { useEffect, useState } from 'react';
import Display from './Components/Display';

function App() {
  const [data, setData] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      if (data) {
        await new Promise(resolve => setTimeout(resolve, 5000));
      }

      await fetch(`./departures.json?_=${new Date().getTime()}`)
        .then(response => {
          if (!response.ok) {
            throw new Error('Network response was not ok');
          }
          return response.json();
        })
        .then((jsonData) => {
          if (data) {
            setData(null);
          } else {
            setData(JSON.stringify(jsonData));
          }
        })
        .catch(error => setError(error.message));
    };

    fetchData();
  });

  return (
    <div className="App">
      {error && <div>Error: {error}</div>}
      {data ? <Display data={JSON.parse(data).data} /> : 'Loading...'}
    </div>
  );
}

export default App;
