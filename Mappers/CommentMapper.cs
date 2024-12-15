using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Author = commentModel.Author,
                Title = commentModel.Title,
                Body = commentModel.Body,
                Date = commentModel.Date,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Author = commentDto.Author,
                Title = commentDto.Title,
                Body = commentDto.Body,
                StockId = stockId
            };
        } 

        public static Comment ToCommentFromUpdateDTO(this UpdateCommentDto commentDto)
        {
            return new Comment
            {
                Author = commentDto.Author,
                Title = commentDto.Title,
                Body = commentDto.Body
            };
        }

    }
}