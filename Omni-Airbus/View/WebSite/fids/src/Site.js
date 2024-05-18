import React, { useEffect, useState, useCallback } from 'react';
import Display from './Components/Display';

/**
 * App component fetches flight departure data from a local JSON file and displays it using the Display component.
 * 
 * State:
 * - data (Array|null): The fetched data as an array. Initially null.
 * - error (string|null): An error message if the fetch operation fails. Initially null.
 * 
 * The component uses useEffect to fetch the data when the component mounts.
 * It includes error handling to display an error message if the fetch fails.
 * If data is successfully fetched, it is passed to the Display component.
 * 
 * Example usage:
 * <App />
 * 
 * @returns {JSX.Element} The rendered component.
 */
export default function App() {
  const [data, setData] = useState(null); // State to hold fetched data
  const [error, setError] = useState(null); // State to hold error message

  const fetchData = useCallback(async () => {
    try {
      const response = await fetch(`./departures.json?_=${new Date().getTime()}`);
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const jsonData = await response.json();
      setData(jsonData.data);
    } catch (error) {
      setError(error.message);
    }
  }, []);

  useEffect(() => {
    fetchData();
    const intervalId = setInterval(fetchData, 60000); // Fetch data every 60 seconds

    return () => clearInterval(intervalId); // Cleanup interval on component unmount
  }, [fetchData]);

  return (
    <div className="App" style={{ background: "black" }}>
      {error && <div>Error: {error}</div>} {/* Display error message if any */}
      {data ? <Display data={data} /> : 'Loading...'} {/* Display data if available, else show loading */}
    </div>
  );
}
