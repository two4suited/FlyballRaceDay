import  { Tournament } from '../models/Tournament'

const API_URL = 'http://localhost:7071/api/'; 

export const getTournaments = async (): Promise<Tournament[]> => {
    const response = await fetch(API_URL + 'GetAll');
    return await response.json();
}
export const createTournament = async (tournament: Tournament): Promise<Tournament> => {
    const response = await fetch(API_URL + 'Create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(tournament)
    });
    return await response.json();
}

export const updateTournament = async (tournament: Tournament): Promise<Tournament> => {
    const response = await fetch(`${API_URL}/${tournament.Id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(tournament)
    });
    return await response.json();
}

export const deleteTournament = async (id: string): Promise<void> => {
    await fetch(`${API_URL}/${id}`, {
        method: 'DELETE'
    });
}