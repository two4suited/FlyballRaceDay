// components/TournamentList.tsx

import React, { useEffect, useState } from 'react';
import { Tournament } from './models/Tournament';
import * as TournamentService from './services/TournamentService';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';

const TournamentList: React.FC = () => {
    const [tournaments, setTournaments] = useState<Tournament[]>([]);

    useEffect(() => {
        const fetchTournaments = async () => {
            const fetchedTournaments = await TournamentService.getTournaments();
            setTournaments(fetchedTournaments);
        };

        fetchTournaments();
    }, []);

    const handleDelete = async (id: string) => {
        try {
            await TournamentService.deleteTournament(id);
            setTournaments(tournaments.filter(t => t.Id !== id)); // Remove the deleted tournament from local state
        } catch (error) {
            console.error('Failed to delete tournament', error);
        }
    };

    return (

        <TableContainer>
            <Table sx={{ minwidth: 650 }} size="small" aria-label="Tournaments">
                <TableHead>
                    <TableRow>
                        <TableCell>Region</TableCell>
                        <TableCell align="right">Event Name</TableCell>
                        <TableCell align="right">Start Date</TableCell>
                        <TableCell align="right">End Date</TableCell>
                        <TableCell align="right">Number of Lanes</TableCell>
                    </TableRow>                    
                </TableHead>
                <TableBody>
                {tournaments.map(t => (
                        <TableRow key={t.Id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                            <TableCell component="th" scope="row">{t.Region}</TableCell>
                            <TableCell align="right">{t.EventName}</TableCell>
                            <TableCell align="right">{new Date(t.StartDate).toLocaleDateString()}</TableCell>
                            <TableCell align="right">{new Date(t.EndDate).toLocaleDateString()}</TableCell>
                            <TableCell align="right">{t.NumberOfLanes}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>  
    );
}

export default TournamentList;
