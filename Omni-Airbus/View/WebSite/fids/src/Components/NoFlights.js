/**
 * NoFlights component renders a full-screen message indicating that no flights are available.
 * 
 * The component displays a centered message.
 * 
 * @returns {JSX.Element} The rendered component.
 */
export default function NoFlights() {
    return (
        <div style={{
            width: "100%",
            height: "100vh",
            display: "flex",
            justifyContent: "center",
            alignContent: "center",
            alignItems: "center",
            background:"Black",
            color:"white"
        }}
        >
            <h1>No Flights Available</h1>
        </div>
    )
}