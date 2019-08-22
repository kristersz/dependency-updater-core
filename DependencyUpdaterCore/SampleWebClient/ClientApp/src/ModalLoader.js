import React from 'react';
import Loader from 'react-loader-spinner'

function ModalLoader() {
    return (
        <div style={{ display: "flex", flexDirection: "column", justifyContent: "center", backgroundColor: "black", opacity: "0.5", position: 'absolute', width: "100%", height: "100%" }}>
            <Loader
                type="Watch"
                color="#00BFFF"
                height="300"
                width="300"
            />
        </div>
    )
}

export default ModalLoader;