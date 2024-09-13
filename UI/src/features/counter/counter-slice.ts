// DUCKS pattern
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface CounterState {
    value: number;
};

const initialState: CounterState = {
    value: 10,
};

const counterSlice = createSlice({
    name: "counter",
    initialState,
    reducers: {
        // methods
        incremented(state) {
            state.value++;
        },
        addAmount(state, action: PayloadAction<number>) {
            state.value += action.payload
        }
    }
});

export const { incremented, addAmount } = counterSlice.actions;
export default counterSlice.reducer;