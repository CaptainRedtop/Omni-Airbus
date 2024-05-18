import { useState } from "react";
import NoFlights from "./NoFlights";
import DisplayItems from "./DisplayItems";

/**
 * Display component manages and renders flight data, showing either a list of flights or a no flights message.
 * 
 * Props:
 * - data (Array): An array of flight objects, where each object contains details about a flight.
 * 
 * The component uses state to manage the flight data and renders a loading message if the data is not yet available.
 * If there are flights in the data, it renders the DisplayItems component; otherwise, it renders the NoFlights component.
 * 
 * @param {Object} props - The component properties.
 * @returns {JSX.Element} The rendered component.
 */
export default function Display(props) {
    const [data, setData] = useState(props.data);

    if (data == null) {
        return (<div>'Loading...'</div>);
    } else {
        return (
            <div style={{ width: "100%" }}>
                {data.length > 0 ? (<DisplayItems data={data} />) : (<NoFlights />)}
            </div>
        );
    }
}