export function generateID() {
    const NUM_CHARS = 4;
    
    let id = "";
    let possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (let i = 0; i < NUM_CHARS; i++) {
        id += possible.charAt(Math.floor(Math.random() * possible.length));
    }

    return id;
}
