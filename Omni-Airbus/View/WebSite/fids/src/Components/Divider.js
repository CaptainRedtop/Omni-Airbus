import React from 'react';

/**
 * Divider component renders a horizontal line with a specified color.
 * 
 * Props:
 * - color (string): The color of the divider line.
 * 
 * @param {Object} props - The component properties.
 * @returns {JSX.Element} The rendered component.
 */
const Divider = (props) => {
    const { color } = props;
    const style = {
        borderTop: `1px solid ${color}`,
        width: "100%",
    };

    return <div style={style}></div>;
};

export default Divider;
